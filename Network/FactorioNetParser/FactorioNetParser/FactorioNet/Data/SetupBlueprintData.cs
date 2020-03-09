using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class SetupBlueprintData : IReadable<SetupBlueprintData>, IWritable<SetupBlueprintData>
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

        public void Write(BinaryWriter writer)
        {
            writer.Write(IncludeModules);
            writer.Write(IncludeEntities);
            writer.Write(IncludeTiles);
            writer.Write(IncludeStationNames);
            writer.Write(IncludeTrains);

            writer.WriteArray(ExcludedEntities, (stream, data) => stream.Write(data));
            writer.WriteArray(ExcludedTiles, (stream, data) => stream.Write(data));
            writer.WriteArray(ExcludedItems, (stream, data) => stream.Write(data));
            writer.WriteArray(Icons);
        }
    }
}