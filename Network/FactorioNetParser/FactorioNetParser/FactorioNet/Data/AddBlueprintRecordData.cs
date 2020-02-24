using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class AddBlueprintRecordData : IReadable<AddBlueprintRecordData>, IWritable<AddBlueprintRecordData>
    {
        public uint AddInBook;
        public SignalId[] BlueprintIcons;
        public byte[] Hash = new byte[20];
        public uint Id;
        public bool IsBook;
        public ushort ItemId;
        public string Label;
        public SingleRecordDataInBook[] PreviewsInBook;

        public AddBlueprintRecordData(BinaryReader reader)
        {
            Load(reader);
        }

        public AddBlueprintRecordData() { }

        public AddBlueprintRecordData Load(BinaryReader reader)
        {
            Id = reader.ReadUInt32();
            Hash = reader.ReadBytes(20);
            ItemId = reader.ReadUInt16();
            IsBook = reader.ReadBoolean();
            BlueprintIcons = reader.ReadArray<SignalId>();
            Label = reader.ReadFactorioString();
            AddInBook = reader.ReadUInt32();
            if (IsBook)
                PreviewsInBook = reader.ReadArray<SingleRecordDataInBook>();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(Hash);
            writer.Write(ItemId);
            writer.Write(IsBook);
            writer.WriteArray(BlueprintIcons);
            writer.WriteFactorioString(Label);
            writer.Write(AddInBook);
            if (IsBook)
                writer.WriteArray(PreviewsInBook);
            throw new System.NotImplementedException();
        }
    }
}