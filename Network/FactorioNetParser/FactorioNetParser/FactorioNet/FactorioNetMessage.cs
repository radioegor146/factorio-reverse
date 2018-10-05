using System.IO;

namespace FactorioNetParser.FactorioNet
{
    public class FactorioNetMessage
    {
        public short CurrentMessageId;
        public short FragmentId;
        public bool IsFragmented;

        public bool IsLastFragment;
        public short LastServerMessageId;
        public short MessageId;
        public byte[] PacketBytes;

        public byte Type;

        public FactorioNetMessage(byte[] packet)
        {
            var stream = new BinaryReader(new MemoryStream(packet));
            var offset = 0;

            Type = stream.ReadByte();
            offset++;
            Type = (byte) (Type & 0b11011111);
            IsFragmented = (Type & 0b01000000) > 0;
            IsLastFragment = (Type & 0b10000000) > 0;
            Type = (byte) (Type & 0b00011111);
            if (Type >= 2 && Type <= 5 || IsFragmented || IsLastFragment)
            {
                MessageId = stream.ReadInt16();
                offset += 2;
                if (IsFragmented)
                {
                    if ((FragmentId = stream.ReadByte()) == 0xFF)
                    {
                        FragmentId = stream.ReadInt16();
                        offset += 2;
                    }

                    offset++;
                }
            }

            if ((MessageId & 0b1000000000000000) > 0)
            {
                stream.ReadByte();
                stream.ReadInt32();
                offset += 5;
            }

            MessageId = (short) (MessageId & 0b0111111111111111);
            PacketBytes = new byte[packet.Length - offset];
            stream.Read(PacketBytes, 0, PacketBytes.Length);
        }

        public FactorioNetMessage(byte type, byte[] data)
        {
            PacketBytes = data;
            Type = type;
        }

        public FactorioNetMessage() { }

        public byte[] GetPacket()
        {
            var ms = new MemoryStream();
            var stream = new BinaryWriter(ms);
            var isReliable = Type >= 2 && Type <= 5;
            if (IsFragmented)
                Type |= 0b01000000;
            if (IsLastFragment)
                Type |= 0b10000000;
            stream.Write(Type);
            if (isReliable || IsFragmented)
            {
                MessageId = CurrentMessageId;
                stream.Write((short) (Type | (LastServerMessageId > 0 ? 0x8000 : 0)));
                if (IsFragmented) stream.WriteVarShort(FragmentId);
            }

            if (LastServerMessageId > 0)
            {
                stream.Write((byte) 1);
                stream.Write((int) LastServerMessageId);
            }

            stream.Write(PacketBytes);
            return ms.ToArray();
        }
    }
}