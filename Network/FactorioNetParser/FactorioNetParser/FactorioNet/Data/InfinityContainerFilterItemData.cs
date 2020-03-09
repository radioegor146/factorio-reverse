using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class InfinityContainerFilterItemData : IReadable<InfinityContainerFilterItemData>, IWritable<InfinityContainerFilterItemData>
    {
        public uint Count;
        public ushort FilterIndex;
        public ushort ItemId;
        public byte Mode;

        public InfinityContainerFilterItemData(BinaryReader reader)
        {
            Load(reader);
        }

        public InfinityContainerFilterItemData() { }

        public InfinityContainerFilterItemData Load(BinaryReader reader)
        {
            ItemId = reader.ReadUInt16();
            Mode = reader.ReadByte();
            FilterIndex = reader.ReadUInt16();
            Count = reader.ReadUInt32();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(ItemId);
            writer.Write(Mode);
            writer.Write(FilterIndex);
            writer.Write(Count);
        }
    }
}