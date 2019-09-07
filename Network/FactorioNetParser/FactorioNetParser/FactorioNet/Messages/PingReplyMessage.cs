using System.IO;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class PingReplyMessage : IMessage<PingReplyMessage>
    {
        public ushort Number;

        public PingReplyMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public PingReplyMessage() { }

        public MessageType GetMessageType() => MessageType.PingReply;

        public PingReplyMessage Load(BinaryReader reader)
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