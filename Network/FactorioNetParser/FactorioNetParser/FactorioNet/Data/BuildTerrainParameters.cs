using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class BuildTerrainParameters : IReadable<BuildTerrainParameters>, IWritable<BuildTerrainParameters>
    {
        public bool CreatedByMoving;
        public bool GhostMode;
        public byte Size;
        public bool SkipFogOfWar;

        public BuildTerrainParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public BuildTerrainParameters() { }

        public BuildTerrainParameters Load(BinaryReader reader)
        {
            CreatedByMoving = reader.ReadBoolean();
            Size = reader.ReadByte();
            GhostMode = reader.ReadBoolean();
            SkipFogOfWar = reader.ReadBoolean();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(CreatedByMoving);
            writer.Write(Size);
            writer.Write(GhostMode);
            writer.Write(SkipFogOfWar);
        }
    }
}