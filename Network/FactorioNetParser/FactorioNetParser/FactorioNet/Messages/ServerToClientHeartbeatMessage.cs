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
            None = 0x00,
            HasRequestsForHeartbeat = 0x01,
            HasTickClosures = 0x02,
            SingleTickClosure = 0x04,
            LoadTickOnly = 0x08,
            HasSynchronizerActions = 0x10,
        }

        public ServerToClientHeartbeatMessage Load(BinaryReader reader)
        {
            var deserializationMask = (DeserializationMaskFlags)reader.ReadByte();
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
            var mask = DeserializationMaskFlags.None;
            if (LoadTickOnly)
                mask |= DeserializationMaskFlags.LoadTickOnly;
            if (TickClosures.Length == 1)
                mask |= DeserializationMaskFlags.SingleTickClosure;
            if (SynchronizerActions != null)
                mask |= DeserializationMaskFlags.HasSynchronizerActions;
            if (RequestsForHeartbeat != null)
                mask |= DeserializationMaskFlags.HasRequestsForHeartbeat;
            writer.Write((byte)mask);
            writer.Write(SequenceNumber);
            if (mask.HasFlag(DeserializationMaskFlags.HasTickClosures))
            {
                if (mask.HasFlag(DeserializationMaskFlags.SingleTickClosure))
                {
                    writer.Write(TickClosures[0].Item1);
                    if (!LoadTickOnly)
                        writer.Write(TickClosures[0].Item2);
                }
                else
                {
                    writer.WriteArray(TickClosures, (stream, obj) =>
                    {
                        stream.Write(obj.Item1);
                        if (!LoadTickOnly)
                            stream.Write(obj.Item2);
                    });
                }
            }

            if (mask.HasFlag(DeserializationMaskFlags.HasSynchronizerActions))
                writer.WriteArray(SynchronizerActions, (stream, obj) =>
                {
                    stream.Write(obj.Item1);
                    stream.Write(obj.Item2);
                });
            if (mask.HasFlag(DeserializationMaskFlags.HasRequestsForHeartbeat))
                writer.WriteArray(RequestsForHeartbeat, (stream, obj) => stream.Write(obj));
        }
    }
}