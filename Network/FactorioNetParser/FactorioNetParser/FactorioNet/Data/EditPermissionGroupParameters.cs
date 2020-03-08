using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class EditPermissionGroupParameters : IReadable<EditPermissionGroupParameters>, IWritable<EditPermissionGroupParameters>
    {
        public byte ActionIndex;
        public int GroupId;
        public string NewGroupName;
        public ushort PlayerIndex;
        public byte Type;

        public EditPermissionGroupParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public EditPermissionGroupParameters() { }

        public EditPermissionGroupParameters Load(BinaryReader reader)
        {
            GroupId = reader.ReadVarInt();
            PlayerIndex = reader.ReadUInt16();
            ActionIndex = reader.ReadByte();
            NewGroupName = reader.ReadFactorioString();
            Type = reader.ReadByte();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(GroupId);
            writer.Write(PlayerIndex);
            writer.Write(ActionIndex);
            writer.WriteFactorioString(NewGroupName);
            writer.Write(Type);
        }
    }
}