using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class TilePosition : IReadable<TilePosition>, IWritable<TilePosition>
    {
        public int X;
        public int Y;

        public TilePosition(BinaryReader reader)
        {
            Load(reader);
        }

        public TilePosition() { }

        public TilePosition Load(BinaryReader reader)
        {
            X = reader.ReadInt32();
            Y = reader.ReadInt32();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
        }
    }
}