using System.IO;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class EmptyMessage : IMessage<EmptyMessage>
    {
        public EmptyMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public EmptyMessage() { }

        public MessageType GetMessageType() => MessageType.Empty;

        public EmptyMessage Load(BinaryReader reader)
        {
            return this;
        }

        public void Write(BinaryWriter writer) { }
    }
}