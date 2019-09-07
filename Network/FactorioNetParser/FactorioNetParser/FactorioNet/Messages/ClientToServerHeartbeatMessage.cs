using System;
using System.IO;
using FactorioNetParser.FactorioNet.Data;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class ClientToServerHeartbeatMessage : IMessage<ClientToServerHeartbeatMessage>
    {
        public bool LoadTickOnly;
        public uint NextReceiveServerTickClosure;
        public uint[] RequestsForHeartbeat;
        public uint SequenceNumber;
        public SynchronizerAction[] SynchronizerActions;
        public Tuple<uint, TickClosure>[] TickClosures;

        public ClientToServerHeartbeatMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public ClientToServerHeartbeatMessage() { }

        [Flags]
        private enum DeserializationMaskFlags : byte
        {
            HasRequestsForHeartbeat = 0x01,
            HasTickClosures = 0x02,
            SingleTickClosure = 0x04,
            LoadTickOnly = 0x08,
            HasSynchronizerActions = 0x10,
        }

        public ClientToServerHeartbeatMessage Load(BinaryReader reader)
        {
            var deserializationMask = (DeserializationMaskFlags) reader.ReadByte();
            SequenceNumber = reader.ReadUInt32();
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

            NextReceiveServerTickClosure = reader.ReadUInt32();

            if (deserializationMask.HasFlag(DeserializationMaskFlags.HasSynchronizerActions))
                SynchronizerActions = reader.ReadArray(x => new SynchronizerAction(x));

            if (deserializationMask.HasFlag(DeserializationMaskFlags.HasRequestsForHeartbeat))
                RequestsForHeartbeat = reader.ReadArray(x => x.ReadUInt32());
            return this;
        }

        public MessageType GetMessageType()
        {
            return MessageType.ClientToServerHeartbeat;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((byte) ((TickClosures != null && TickClosures.Length > 0
                                     ? DeserializationMaskFlags.HasTickClosures
                                     : 0) |
                                 (TickClosures != null && TickClosures.Length > 1
                                     ? DeserializationMaskFlags.SingleTickClosure
                                     : 0) |
                                 (LoadTickOnly ? DeserializationMaskFlags.LoadTickOnly : 0) |
                                 (SynchronizerActions != null && SynchronizerActions.Length > 0
                                     ? DeserializationMaskFlags.HasSynchronizerActions
                                     : 0) |
                                 (RequestsForHeartbeat != null && RequestsForHeartbeat.Length > 0
                                     ? DeserializationMaskFlags.HasRequestsForHeartbeat
                                     : 0)));
            writer.Write(SequenceNumber);

            if (TickClosures != null && TickClosures.Length > 0)
            {
                if (TickClosures.Length == 1)
                {
                    writer.Write(TickClosures[0].Item1);
                    if (!LoadTickOnly)
                        TickClosures[0].Item2.Write(writer);
                }
                else
                {
                    writer.WriteArray(TickClosures, (x, y) =>
                    {
                        x.Write(y.Item1);
                        if (!LoadTickOnly)
                            y.Item2.Write(x);
                    });
                }
            }

            if (SynchronizerActions != null && SynchronizerActions.Length > 0)
                writer.WriteArray(SynchronizerActions);

            if (RequestsForHeartbeat != null && RequestsForHeartbeat.Length > 0)
                writer.WriteArray(RequestsForHeartbeat, (x, y) => x.Write(y));

            writer.Write(NextReceiveServerTickClosure);
        }
    }
}