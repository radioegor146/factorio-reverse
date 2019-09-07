using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class UpdateBlueprintShelfData : IReadable<UpdateBlueprintShelfData>
    {
        public uint NextRecordId;
        public AddBlueprintRecordData[] RecordsToAdd;
        public ushort[] RecordsToRemove;
        public UpdateBlueprintData[] RecordsToUpdate;
        public ushort ShelfPlayerIndex;
        public uint Timestamp;

        public UpdateBlueprintShelfData(BinaryReader reader)
        {
            Load(reader);
        }

        public UpdateBlueprintShelfData() { }

        public UpdateBlueprintShelfData Load(BinaryReader reader)
        {
            ShelfPlayerIndex = reader.ReadUInt16();
            NextRecordId = reader.ReadUInt32();
            Timestamp = reader.ReadUInt32();
            RecordsToRemove = reader.ReadArray(x => x.ReadUInt16());
            RecordsToAdd = reader.ReadArray<AddBlueprintRecordData>();
            RecordsToUpdate = reader.ReadArray<UpdateBlueprintData>();
            return this;
        }
    }
}