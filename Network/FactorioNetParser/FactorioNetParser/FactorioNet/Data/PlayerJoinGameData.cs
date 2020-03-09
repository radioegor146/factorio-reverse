using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class PlayerJoinGameData : IReadable<PlayerJoinGameData>, IWritable<PlayerJoinGameData>
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

        public void Write(BinaryWriter writer)
        {
            writer.WriteVarShort((short)PeerId);
            writer.Write(PlayerId);
            writer.Write(ForceId);
            writer.WriteFactorioString(Username);
            writer.Write(AsEditor);
            writer.Write(Admin);
        }
    }
}