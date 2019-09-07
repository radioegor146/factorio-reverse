using System.IO;
using FactorioNetParser.FactorioNet.Data;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class ConnectionRequestReplyConfirmMessage : IMessage<ConnectionRequestReplyConfirmMessage>
    {
        public int ConnectionRequestIdGeneratedOnClient;
        public int ConnectionRequestIdGeneratedOnServer;
        public int CoreChecksum;
        public int InstanceId;
        public ModInfo[] Mods = new ModInfo[0];
        public ModSetting[] ModSettings = new ModSetting[0];
        public string PasswordHash = "";
        public string ServerKey = "";
        public string ServerKeyTimestamp = "";
        public string Username = "factoriolauncher";

        public ConnectionRequestReplyConfirmMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public ConnectionRequestReplyConfirmMessage() { }

        public MessageType GetMessageType() => MessageType.ConnectionRequestReplyConfirm;

        public ConnectionRequestReplyConfirmMessage Load(BinaryReader reader)
        {
            ConnectionRequestIdGeneratedOnClient = reader.ReadInt32();
            ConnectionRequestIdGeneratedOnServer = reader.ReadInt32();
            InstanceId = reader.ReadInt32();
            Username = reader.ReadSimpleString();
            PasswordHash = reader.ReadSimpleString();
            ServerKey = reader.ReadSimpleString();
            ServerKeyTimestamp = reader.ReadSimpleString();
            CoreChecksum = reader.ReadInt32();
            Mods = reader.ReadArray<ModInfo>();
            ModSettings = reader.ReadArray<ModSetting>();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(ConnectionRequestIdGeneratedOnClient);
            writer.Write(ConnectionRequestIdGeneratedOnServer);
            writer.Write(InstanceId);
            writer.WriteSimpleString(Username);
            writer.WriteSimpleString(PasswordHash);
            writer.WriteSimpleString(ServerKey);
            writer.WriteSimpleString(ServerKeyTimestamp);
            writer.Write(CoreChecksum);
            writer.WriteArray(Mods);
            writer.WriteArray(ModSettings);
        }
    }
}