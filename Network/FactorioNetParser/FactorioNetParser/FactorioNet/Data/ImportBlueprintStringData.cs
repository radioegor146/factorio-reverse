using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class ImportBlueprintStringData : IReadable<ImportBlueprintStringData>, IWritable<ImportBlueprintStringData>
    {
        public string StringData;
        public byte ImportType;
        public byte TextDisplay;
        public byte FromChat;

        public ImportBlueprintStringData(BinaryReader reader)
        {
            Load(reader);
        }

        public ImportBlueprintStringData() { }

        public ImportBlueprintStringData Load(BinaryReader reader)
        {
            StringData = reader.ReadFactorioString();
            ImportType = reader.ReadByte();
            TextDisplay = reader.ReadByte();
            FromChat = reader.ReadByte();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteFactorioString(StringData);
            writer.Write(ImportType);
            writer.Write(TextDisplay);
            writer.Write(FromChat);
        }
    }
}