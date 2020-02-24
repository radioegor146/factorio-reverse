using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class Direction : IReadable<Direction>, IWritable<Direction>
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

        public void Write(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }
}