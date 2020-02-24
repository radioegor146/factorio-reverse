using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class AdminAction : IReadable<AdminAction>, IWritable<AdminAction>
    {
        public ushort PlayerId;
        public string Username;
        public uint NewGroupId;
        public byte Action;

        public AdminAction(BinaryReader reader)
        {
            Load(reader);
        }

        public AdminAction() { }

        public AdminAction Load(BinaryReader reader)
        {
            PlayerId = reader.ReadUInt16();
            Username = reader.ReadFactorioString();
            NewGroupId = reader.ReadUInt32();
            Action = reader.ReadByte();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(PlayerId);
            writer.WriteFactorioString(Username);
            writer.Write(NewGroupId);
            writer.Write(Action);
        }
    }
}