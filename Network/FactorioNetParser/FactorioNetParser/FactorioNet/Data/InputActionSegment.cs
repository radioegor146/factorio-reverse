using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class InputActionSegment : IReadable<InputActionSegment>, IWritable<InputActionSegment>
    {
        public byte[] Payload;
        public int FragmentNumber;
        public int Id;
        public short PlayerIndex = -1;
        public int TotalFragments = 1;
        public InputActionType Type;

        public InputActionSegment(BinaryReader reader)
        {
            Load(reader);
        }

        public InputActionSegment() { }

        public InputActionSegment Load(BinaryReader reader)
        {
            Type = (InputActionType) reader.ReadByte();

            if (Type.IsSegmentable())
            {
                Id = reader.ReadInt32();
                PlayerIndex = reader.ReadVarShort();
                TotalFragments = reader.ReadVarInt();
                FragmentNumber = reader.ReadVarInt();
            }

            Payload = reader.ReadBytes(reader.ReadVarInt());
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((byte)Type);
            if (Type.IsSegmentable())
            {
                writer.Write(Id);
                writer.WriteVarShort(PlayerIndex);
                writer.WriteVarInt(TotalFragments);
                writer.WriteVarInt(FragmentNumber);
            }

            writer.WriteVarInt(Payload.Length);
            writer.Write(Payload);
        }
    }
}