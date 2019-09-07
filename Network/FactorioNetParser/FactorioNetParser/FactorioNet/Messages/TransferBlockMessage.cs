using System.IO;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class TransferBlockMessage : IMessage<TransferBlockMessage>
    {
        public uint BlockNumber;
        public byte[] Data;

        public TransferBlockMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public TransferBlockMessage() { }

        public MessageType GetMessageType() => MessageType.TransferBlock;

        public TransferBlockMessage Load(BinaryReader reader)
        {
            BlockNumber = reader.ReadUInt32();
            Data = reader.ReadBytes(503);
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(BlockNumber);
            if (Data.Length != 503)
                throw new IOException($"Data size != 503: {Data.Length}");
            writer.Write(Data);
        }
    }
}