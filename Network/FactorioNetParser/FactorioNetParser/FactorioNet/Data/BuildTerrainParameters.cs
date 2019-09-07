using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class BuildTerrainParameters : IReadable<BuildTerrainParameters>
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
    }
}