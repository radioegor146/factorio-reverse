using System.IO;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class ClientToServerHeartbeatMessage : IReadable<ClientToServerHeartbeatMessage>, IPacket
    {
        public byte DeserializationMask;
        public int NextReceiveServerTickClosure;
        public uint[] RequestsForHeartbeat;
        public int SequenceNumber;
        public SynchronizerAction[] SynchronizerActions;
        public TickClosure[] TickClosures;

        public ClientToServerHeartbeatMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public ClientToServerHeartbeatMessage() { }

        public ClientToServerHeartbeatMessage Load(BinaryReader reader)
        {
            DeserializationMask = reader.ReadByte();
            SequenceNumber = reader.ReadInt32();
            if ((DeserializationMask & 0x02) > 0)
            {
                var loadTickOnly = (DeserializationMask & 0x08) > 0;
                if ((DeserializationMask & 0x04) > 0)
                {
                    TickClosures = new TickClosure[1];
                    TickClosures[0] = new TickClosure(reader, loadTickOnly);
                }
                else
                    TickClosures = reader.ReadArray(x => new TickClosure(x, loadTickOnly));
            }

            NextReceiveServerTickClosure = reader.ReadInt32();

            if ((DeserializationMask & 0x10) > 0)
                SynchronizerActions = reader.ReadArray(x => new SynchronizerAction(x));

            if ((DeserializationMask & 0x01) > 0)
                RequestsForHeartbeat = reader.ReadArray(x => x.ReadUInt32());
            return this;
        }

        public PacketType GetMessageType()
        {
            return PacketType.ClientToServerHeartbeat;
        }
    }
}