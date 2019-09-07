using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class InputActionSegment : IReadable<InputActionSegment>
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
    }
}