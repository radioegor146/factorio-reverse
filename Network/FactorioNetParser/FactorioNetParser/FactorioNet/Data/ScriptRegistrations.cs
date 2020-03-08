using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class ScriptRegistrations : IReadable<ScriptRegistrations>, IWritable<ScriptRegistrations>
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

        public void Write(BinaryWriter writer)
        {
            writer.WriteArray(StandardEvents, (stream, data) => stream.Write(data));
            writer.WriteArray(StandardEvents, (stream, data) => stream.Write(data));
            writer.Write(OnInit);
            writer.Write(OnLoad);
            writer.Write(OnConfigurationChanged);
        }
    }
}