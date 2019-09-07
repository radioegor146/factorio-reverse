using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class UpdateBlueprintData : IReadable<UpdateBlueprintData>
    {
        public ushort Id;
        public byte[] NewHash = new byte[20];
        public string NewLabel;

        public UpdateBlueprintData(BinaryReader reader)
        {
            Load(reader);
        }

        public UpdateBlueprintData() { }

        public UpdateBlueprintData Load(BinaryReader reader)
        {
            Id = reader.ReadUInt16();
            NewHash = reader.ReadBytes(20);
            NewLabel = reader.ReadFactorioString();
            return this;
        }
    }
}