using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class Vector : IReadable<Vector>
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
    }
}