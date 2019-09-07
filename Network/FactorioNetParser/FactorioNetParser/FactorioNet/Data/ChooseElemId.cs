﻿using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class ChooseElemId : IReadable<ChooseElemId>
    {
        public ushort EntityId;
        public ushort FluidId;
        public ushort ItemId;
        public ushort RecipeId;
        public SignalId SignalId;
        public ushort TileId;

        public ChooseElemId(BinaryReader reader)
        {
            Load(reader);
        }

        public ChooseElemId() { }

        public ChooseElemId Load(BinaryReader reader)
        {
            ItemId = reader.ReadUInt16();
            EntityId = reader.ReadUInt16();
            TileId = reader.ReadUInt16();
            FluidId = reader.ReadUInt16();
            RecipeId = reader.ReadUInt16();
            SignalId = new SignalId(reader);
            return this;
        }
    }
}