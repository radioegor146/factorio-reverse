using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class Vector : IReadable<Vector>, IWritable<Vector>
    {
        public double X;
        public double Y;

        public Vector(BinaryReader reader)
        {
            Load(reader);
        }

        public Vector() { }

        public Vector Load(BinaryReader reader)
        {
            X = reader.ReadDouble();
            Y = reader.ReadDouble();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
        }
    }
}