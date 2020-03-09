using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class ItemStackTargetSpecification : IReadable<ItemStackTargetSpecification>, IWritable<ItemStackTargetSpecification>
    {
        public byte InventoryIndex;
        public ushort SlotIndex;
        public byte Source;
        public byte Target;

        public ItemStackTargetSpecification(BinaryReader reader)
        {
            Load(reader);
        }

        public ItemStackTargetSpecification() { }

        public ItemStackTargetSpecification Load(BinaryReader reader)
        {
            InventoryIndex = reader.ReadByte();
            SlotIndex = reader.ReadUInt16();
            Source = reader.ReadByte();
            Target = reader.ReadByte();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(InventoryIndex);
            writer.Write(SlotIndex);
            writer.Write(Source);
            writer.Write(Target);
        }
    }
}