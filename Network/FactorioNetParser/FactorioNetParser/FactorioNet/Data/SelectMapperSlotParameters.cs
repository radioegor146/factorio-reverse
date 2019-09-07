using System;
using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class SelectMapperSlotParameters : IReadable<SelectMapperSlotParameters>
    {
        public byte Type;
        public ushort ItemIdIndex;
        public ushort EntityIdIndex;
        public ushort Index;
        public bool IsTo;

        public SelectMapperSlotParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public SelectMapperSlotParameters() { }

        public SelectMapperSlotParameters Load(BinaryReader reader)
        {
            Type = reader.ReadByte();
            if (Type > 1)
                throw new ArgumentOutOfRangeException($"Invalid UpgradeID type: {Type}");
            if (Type == 1)
                ItemIdIndex = reader.ReadUInt16();
            else
                EntityIdIndex = reader.ReadUInt16();
            Index = reader.ReadUInt16();
            IsTo = reader.ReadBoolean();
            return this;
        }
    }
}