using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class SingleRecordDataInBook : IReadable<SingleRecordDataInBook>
    {
        public SignalId[] BlueprintIcons;
        public byte[] Hash = new byte[20];
        public uint Id;
        public ushort ItemId;
        public string Label;

        public SingleRecordDataInBook(BinaryReader reader)
        {
            Load(reader);
        }

        public SingleRecordDataInBook() { }

        public SingleRecordDataInBook Load(BinaryReader reader)
        {
            Id = reader.ReadUInt32();
            ItemId = reader.ReadUInt16();

            Hash = reader.ReadBytes(20);
            BlueprintIcons = reader.ReadArray<SignalId>();
            Label = reader.ReadFactorioString();
            return this;
        }
    }
}