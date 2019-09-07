﻿using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class MapPosition : IReadable<MapPosition>
    {
        public int X;
        public int Y;

        public MapPosition(BinaryReader reader)
        {
            Load(reader);
        }

        public MapPosition() { }

        public MapPosition Load(BinaryReader reader)
        {
            X = reader.ReadInt32();
            Y = reader.ReadInt32();
            return this;
        }
    }
}