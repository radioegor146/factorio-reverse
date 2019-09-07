using System.IO;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class TransferBlockRequestMessage : IMessage<TransferBlockRequestMessage>
    {
        public uint BlockNumber;

        public TransferBlockRequestMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public TransferBlockRequestMessage() { }

        public MessageType GetMessageType() => MessageType.TransferBlockRequest;

        public TransferBlockRequestMessage Load(BinaryReader reader)
        {
            BlockNumber = reader.ReadUInt32();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(BlockNumber);
        }
    }
}