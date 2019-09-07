using System.IO;
using FactorioNetParser.FactorioNet.Data;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class ConnectionRequestReplyMessage : IMessage<ConnectionRequestReplyMessage>
    {
        public ushort BuildVersion;
        public int ConnectionRequestIdGeneratedOnClient;
        public int ConnectionRequestIdGeneratedOnServer;
        public Version Version;

        public ConnectionRequestReplyMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public MessageType GetMessageType() => MessageType.ConnectionRequestReply;

        public ConnectionRequestReplyMessage Load(BinaryReader reader)
        {
            Version = new Version(reader);
            BuildVersion = reader.ReadUInt16();
            ConnectionRequestIdGeneratedOnClient = reader.ReadInt32();
            ConnectionRequestIdGeneratedOnServer = reader.ReadInt32();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            
        }
    }
}