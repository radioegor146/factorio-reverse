using System;
using System.IO;
using FactorioNetParser.FactorioNet.Data;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class ServerToClientHeartbeatMessage : IMessage<ServerToClientHeartbeatMessage>
    {
        public bool LoadTickOnly;
        public uint[] RequestsForHeartbeat;
        public int SequenceNumber;
        public Tuple<SynchronizerAction, ushort>[] SynchronizerActions;
        public Tuple<uint, TickClosure>[] TickClosures;

        public ServerToClientHeartbeatMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public ServerToClientHeartbeatMessage() { }

        public MessageType GetMessageType()
        {
            return MessageType.ServerToClientHeartbeat;
        }

        [Flags]
        private enum DeserializationMaskFlags : byte
        {
            HasRequestsForHeartbeat = 0x01,
            HasTickClosures = 0x02,
            SingleTickClosure = 0x04,
            LoadTickOnly = 0x08,
            HasSynchronizerActions = 0x10,
        }

        public ServerToClientHeartbeatMessage Load(BinaryReader reader)
        {
            var deserializationMask = (DeserializationMaskFlags) reader.ReadByte();
            SequenceNumber = reader.ReadInt32();
            if (deserializationMask.HasFlag(DeserializationMaskFlags.HasTickClosures))
            {
                LoadTickOnly = deserializationMask.HasFlag(DeserializationMaskFlags.LoadTickOnly);
                if (deserializationMask.HasFlag(DeserializationMaskFlags.SingleTickClosure))
                {
                    TickClosures = new[]
                    {
                        new Tuple<uint, TickClosure>( reader.ReadUInt32(), LoadTickOnly ? null : new TickClosure(reader) )
                    };
                }
                else
                {
                    TickClosures = reader.ReadArray(x => new Tuple<uint, TickClosure>(reader.ReadUInt32(),
                        LoadTickOnly ? null : new TickClosure(x)));
                }
            }

            if (deserializationMask.HasFlag(DeserializationMaskFlags.HasSynchronizerActions))
                SynchronizerActions = reader.ReadArray(x => Tuple.Create(new SynchronizerAction(x), x.ReadUInt16()));

            if (deserializationMask.HasFlag(DeserializationMaskFlags.HasRequestsForHeartbeat))
                RequestsForHeartbeat = reader.ReadArray(x => x.ReadUInt32());
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}