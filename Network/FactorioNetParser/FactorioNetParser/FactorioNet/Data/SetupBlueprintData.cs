using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class SetupBlueprintData : IReadable<SetupBlueprintData>
    {
        public uint[] ExcludedEntities;
        public uint[] ExcludedTiles;
        public ushort[] ExcludedItems;
        public SignalId[] Icons;
        public bool IncludeEntities;
        public bool IncludeModules;
        public bool IncludeStationNames;
        public bool IncludeTiles;
        public bool IncludeTrains;

        public SetupBlueprintData(BinaryReader reader)
        {
            Load(reader);
        }

        public SetupBlueprintData() { }

        public SetupBlueprintData Load(BinaryReader reader)
        {
            IncludeModules = reader.ReadBoolean();
            IncludeEntities = reader.ReadBoolean();
            IncludeTiles = reader.ReadBoolean();
            IncludeStationNames = reader.ReadBoolean();
            IncludeTrains = reader.ReadBoolean();

            ExcludedEntities = reader.ReadArray(x => x.ReadUInt32());
            ExcludedTiles = reader.ReadArray(x => x.ReadUInt32());
            ExcludedItems = reader.ReadArray(x => x.ReadUInt16());
            Icons = reader.ReadArray<SignalId>();
            return this;
        }
    }
}