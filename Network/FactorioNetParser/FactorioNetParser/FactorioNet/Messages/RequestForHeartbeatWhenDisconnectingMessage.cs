using System.IO;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class RequestForHeartbeatWhenDisconnectingMessage : IMessage<RequestForHeartbeatWhenDisconnectingMessage>
    {
        public uint NextSequenceId;
        public uint[] Requests;

        public RequestForHeartbeatWhenDisconnectingMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public RequestForHeartbeatWhenDisconnectingMessage() { }

        public MessageType GetMessageType() => MessageType.RequestForHeartbeatWhenDisconnecting;

        public RequestForHeartbeatWhenDisconnectingMessage Load(BinaryReader reader)
        {
            NextSequenceId = reader.ReadUInt32();
            Requests = reader.ReadArray(x => x.ReadUInt32());
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(NextSequenceId);
            writer.WriteArray(Requests, (x, y) => x.Write(y));
        }
    }
}