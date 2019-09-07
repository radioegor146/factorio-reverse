using System.IO;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class GameInformationRequestMessage : IMessage<GameInformationRequestMessage>
    {
        public ushort Number;

        public GameInformationRequestMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public GameInformationRequestMessage() { }

        public MessageType GetMessageType() => MessageType.LanBroadcast;

        public GameInformationRequestMessage Load(BinaryReader reader)
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