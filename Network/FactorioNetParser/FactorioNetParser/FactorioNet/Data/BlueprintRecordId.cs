using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class BlueprintRecordId : IReadable<BlueprintRecordId>, IWritable<BlueprintRecordId>
    {
        public uint Id;
        public ushort PlayerIndex;

        public BlueprintRecordId(BinaryReader reader)
        {
            Load(reader);
        }

        public BlueprintRecordId() { }

        public BlueprintRecordId Load(BinaryReader reader)
        {
            PlayerIndex = reader.ReadUInt16();
            Id = reader.ReadUInt32();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(PlayerIndex);
            writer.Write(Id);
        }
    }
}