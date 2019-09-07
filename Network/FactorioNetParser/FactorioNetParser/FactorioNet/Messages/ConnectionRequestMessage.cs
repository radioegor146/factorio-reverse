using System.IO;
using FactorioNetParser.FactorioNet.Data;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class ConnectionRequestMessage : IMessage<ConnectionRequestMessage>
    {
        public ushort BuildVersion;
        public int ConnectionRequestIdGeneratedOnClient;
        public Version Version;

        public ConnectionRequestMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public ConnectionRequestMessage() { }

        public MessageType GetMessageType() => MessageType.ConnectionRequest;

        public ConnectionRequestMessage Load(BinaryReader reader)
        {
            Version = new Version(reader);
            BuildVersion = reader.ReadUInt16();
            ConnectionRequestIdGeneratedOnClient = reader.ReadInt32();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            Version.Write(writer);
            writer.Write(BuildVersion);
            writer.Write(ConnectionRequestIdGeneratedOnClient);
        }
    }
}