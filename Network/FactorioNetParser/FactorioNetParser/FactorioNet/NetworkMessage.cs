using System.IO;

namespace FactorioNetParser.FactorioNet
{
    public class NetworkMessage
    {
        public ushort FragmentId;
        public bool IsFragmented;

        public bool IsLastFragment;
        public ushort MessageId;
        public byte[] MessageDataBytes;

        public uint[] Confirm;

        public MessageType Type;

        public NetworkMessage(byte[] packet)
        {
            var reader = new BinaryReader(new MemoryStream(packet));

            var type = reader.ReadByte();
            type = (byte) (type & 0b11011111);
            IsFragmented = (type & 0b01000000) > 0;
            IsLastFragment = (type & 0b10000000) > 0;
            Type = (MessageType) (type & 0b00011111);
            if (Type.IsAlwaysReliable() || IsFragmented || IsLastFragment)
            {
                MessageId = reader.ReadUInt16();

                if (IsFragmented)
                {
                    if ((FragmentId = reader.ReadByte()) == 0xFF)
                    {
                        FragmentId = reader.ReadUInt16();
                    }
                }

                if ((MessageId & 0x8000) > 0)
                {
                    Confirm = reader.ReadArray(x => x.ReadUInt32());
                }

                MessageId = (ushort)(MessageId & 0x7FFF);
            }

            MessageDataBytes = new byte[reader.BaseStream.Length - reader.BaseStream.Position];
            reader.Read(MessageDataBytes, 0, MessageDataBytes.Length);
        }

        public NetworkMessage(MessageType type, byte[] data)
        {
            MessageDataBytes = data;
            Type = type;
        }

        public NetworkMessage() { }

        public byte[] GetPacket()
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            var type = (byte) Type;
            if (IsFragmented)
                type |= 0b01000000;
            if (IsFragmented && IsLastFragment)
                type |= 0b10000000;
            writer.Write(type);
            if (Type.IsAlwaysReliable() || IsFragmented)
            {
                writer.Write((short) (MessageId | (Confirm != null && Confirm.Length > 0 ? 0x8000 : 0)));
                if (IsFragmented)
                    writer.WriteVarShort((short)FragmentId);
                if (Confirm != null && Confirm.Length > 0)
                    writer.WriteArray(Confirm, (x, y) => x.Write(y));
            }

            writer.Write(MessageDataBytes);
            return stream.ToArray();
        }
    }
}