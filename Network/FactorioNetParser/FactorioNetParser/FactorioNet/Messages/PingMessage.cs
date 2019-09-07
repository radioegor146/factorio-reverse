using System.IO;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class PingMessage : IMessage<PingMessage>
    {
        public ushort Number;

        public PingMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public PingMessage() { }

        public MessageType GetMessageType() => MessageType.Ping;

        public PingMessage Load(BinaryReader reader)
        {
            Number = reader.ReadUInt16();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Number);
        }
    }
}