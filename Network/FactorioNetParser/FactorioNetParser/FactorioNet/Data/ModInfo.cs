using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class ModInfo : IReadable<ModInfo>, IWritable<ModInfo>
    {
        public int Crc;
        public string Name;
        public Version Version;

        public ModInfo(BinaryReader reader)
        {
            Load(reader);
        }

        public ModInfo() { }

        public ModInfo Load(BinaryReader reader)
        {
            Name = reader.ReadSimpleString();
            Version = new Version(reader);
            Crc = reader.ReadInt32();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteSimpleString(Name);
            Version.Write(writer);
            writer.Write(Crc);
        }
    }
}