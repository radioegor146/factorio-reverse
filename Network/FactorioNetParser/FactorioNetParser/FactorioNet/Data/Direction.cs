using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class Direction : IReadable<Direction>
    {
        public byte Value;

        public Direction(BinaryReader reader)
        {
            Load(reader);
        }

        public Direction() { }

        public Direction Load(BinaryReader reader)
        {
            Value = reader.ReadByte();
            return this;
        }
    }
}