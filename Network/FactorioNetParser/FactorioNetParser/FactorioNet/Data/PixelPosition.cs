using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class PixelPosition : IReadable<PixelPosition>, IWritable<PixelPosition>
    {
        public int X;
        public int Y;

        public PixelPosition(BinaryReader reader)
        {
            Load(reader);
        }

        public PixelPosition() { }

        public PixelPosition Load(BinaryReader reader)
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