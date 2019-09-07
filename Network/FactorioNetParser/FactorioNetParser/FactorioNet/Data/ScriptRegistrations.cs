using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class ScriptRegistrations : IReadable<ScriptRegistrations>
    {
        public uint[] StandardEvents;
        public uint[] NthTickEvents;
        public byte OnInit;
        public byte OnLoad;
        public byte OnConfigurationChanged;

        public ScriptRegistrations() { }

        public ScriptRegistrations(BinaryReader reader)
        {
            Load(reader);
        }

        public ScriptRegistrations Load(BinaryReader reader)
        {
            StandardEvents = reader.ReadArray(x => x.ReadUInt32());
            NthTickEvents = reader.ReadArray(x => x.ReadUInt32());
            OnInit = reader.ReadByte();
            OnLoad = reader.ReadByte();
            OnConfigurationChanged = reader.ReadByte();
            return this;
        }
    }
}