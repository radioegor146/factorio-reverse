using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class PlayerJoinGameData : IReadable<PlayerJoinGameData>
    {
        public ushort PeerId;
        public ushort PlayerId;
        public byte ForceId;
        public string Username;
        public bool AsEditor;
        public bool Admin;

        public PlayerJoinGameData() { }

        public PlayerJoinGameData(BinaryReader reader)
        {
            Load(reader);
        }

        public PlayerJoinGameData Load(BinaryReader reader)
        {
            PeerId = (ushort) reader.ReadVarShort();
            PlayerId = reader.ReadUInt16();
            ForceId = reader.ReadByte();
            Username = reader.ReadFactorioString();
            AsEditor = reader.ReadBoolean();
            Admin = reader.ReadBoolean();
            return this;
        }
    }
}