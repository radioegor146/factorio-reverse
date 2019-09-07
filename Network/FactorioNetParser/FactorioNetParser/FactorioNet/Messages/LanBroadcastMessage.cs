using System.IO;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class LanBroadcastMessage : IMessage<LanBroadcastMessage>
    {
        public ushort Number;

        public LanBroadcastMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public LanBroadcastMessage() { }

        public MessageType GetMessageType() => MessageType.LanBroadcast;

        public LanBroadcastMessage Load(BinaryReader reader)
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