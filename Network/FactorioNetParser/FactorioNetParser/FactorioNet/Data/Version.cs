using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class Version : IReadable<Version>, IWritable<Version>
    {
        public short MajorVersion;
        public short MinorVersion;
        public short SubVersion;

        public Version(BinaryReader reader)
        {
            Load(reader);
        }

        public Version() { }

        public Version Load(BinaryReader reader)
        {
            MajorVersion = reader.ReadVarShort();
            MinorVersion = reader.ReadVarShort();
            SubVersion = reader.ReadVarShort();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteVarShort(MajorVersion);
            writer.WriteVarShort(MinorVersion);
            writer.WriteVarShort(SubVersion);
        }
    }
}