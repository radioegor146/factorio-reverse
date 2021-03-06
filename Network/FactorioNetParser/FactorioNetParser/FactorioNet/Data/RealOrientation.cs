﻿using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class RealOrientation : IReadable<RealOrientation>, IWritable<RealOrientation>
    {
        public float Orientation;

        public RealOrientation(BinaryReader reader)
        {
            Load(reader);
        }

        public RealOrientation() { }

        public RealOrientation Load(BinaryReader reader)
        {
            Orientation = reader.ReadSingle();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Orientation);
        }
    }
}