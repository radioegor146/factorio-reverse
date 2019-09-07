using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FactorioNetParser.FactorioNet.Messages;

namespace FactorioNetParser.FactorioNet
{
    internal class NetworkClient
    {
        public const int MaxSinglePacketSize = 503;

        private UdpClient client;
        private readonly IPEndPoint endPoint;
        
        private readonly Dictionary<ushort, NetworkMessageBundle> fragmentedMessages =
            new Dictionary<ushort, NetworkMessageBundle>();

        public delegate void MessageReceivedCallback(IMessage<object> message);

        public event MessageReceivedCallback OnMessageReceived;

        private long lastFuckedUpId;

        private readonly List<ushort> idsToConfirm = new List<ushort>();
        private ushort lastMessageId;

        public NetworkClient(IPEndPoint endPoint)
        {
            this.endPoint = endPoint;
        }

        public void Open()
        {
            idsToConfirm.Clear();
            client = new UdpClient();
            client.Connect(endPoint);
            client.BeginReceive(ReceiveCallback, null);
        }

        private void HandleRawMessage(byte[] messageData)
        {
            lock (this)
            {
                var networkMessage = new NetworkMessage(messageData);
                if (networkMessage.Type.IsAlwaysReliable() || networkMessage.IsFragmented)
                    idsToConfirm.Add(networkMessage.MessageId);
                if (networkMessage.IsFragmented)
                {
                    if (!fragmentedMessages.ContainsKey(networkMessage.MessageId))
                        fragmentedMessages.Add(networkMessage.MessageId, new NetworkMessageBundle());
                    if (!fragmentedMessages[networkMessage.MessageId].HandleBundleMessage(networkMessage))
                        return;
                    HandleMessage(networkMessage.Type,
                        fragmentedMessages[networkMessage.MessageId].GetOverallMessage().MessageDataBytes);
                    fragmentedMessages.Remove(networkMessage.MessageId);
                }
                else
                {
                    HandleMessage(networkMessage.Type, networkMessage.MessageDataBytes);
                }
            }
        }

        private void HandleMessage(MessageType type, byte[] data)
        {
            var dataStream = new MemoryStream(data);
            IMessage<object> resultMessage = null;
            switch (type)
            {
                case MessageType.ServerToClientHeartbeat:
                    resultMessage = new ServerToClientHeartbeatMessage(new BinaryReader(dataStream));
                    break;
                case MessageType.ClientToServerHeartbeat:
                    resultMessage = new ClientToServerHeartbeatMessage(new BinaryReader(dataStream));
                    break;
                case MessageType.Ping:
                    resultMessage = new PingMessage(new BinaryReader(dataStream));
                    break;
                case MessageType.PingReply:
                    resultMessage = new PingReplyMessage(new BinaryReader(dataStream));
                    break;
                case MessageType.ConnectionRequest:
                    resultMessage = new ConnectionRequestMessage(new BinaryReader(dataStream));
                    break;
                case MessageType.ConnectionRequestReply:
                    resultMessage = new ConnectionRequestReplyMessage(new BinaryReader(dataStream));
                    break;
                case MessageType.ConnectionRequestReplyConfirm:
                    resultMessage = new ConnectionRequestReplyConfirmMessage(new BinaryReader(dataStream));
                    break;
                case MessageType.ConnectionAcceptOrDeny:
                    resultMessage = new ConnectionAcceptOrDenyMessage(new BinaryReader(dataStream));
                    break;
                case MessageType.GetOwnAddress:
                    break;
                case MessageType.GetOwnAddressReply:
                    break;
                case MessageType.NatPunchRequest:
                    break;
                case MessageType.NatPunch:
                    break;
                case MessageType.TransferBlockRequest:
                    resultMessage = new TransferBlockRequestMessage(new BinaryReader(dataStream));
                    break;
                case MessageType.TransferBlock:
                    resultMessage = new TransferBlockMessage(new BinaryReader(dataStream));
                    break;
                case MessageType.RequestForHeartbeatWhenDisconnecting:
                    resultMessage = new RequestForHeartbeatWhenDisconnectingMessage(new BinaryReader(dataStream));
                    break;
                case MessageType.LanBroadcast:
                    resultMessage = new LanBroadcastMessage(new BinaryReader(dataStream));
                    break;
                case MessageType.GameInformationRequest:
                    resultMessage = new GameInformationRequestMessage(new BinaryReader(dataStream));
                    break;
                case MessageType.GameInformationRequestReply:
                    break;
                case MessageType.Empty:
                    resultMessage = new EmptyMessage(new BinaryReader(dataStream));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            OnMessageReceived?.Invoke(resultMessage);

            if (dataStream.Position == dataStream.Length)
                return;

            Console.WriteLine($"Fucked up: {dataStream.Position}/{dataStream.Length}; Type: {type}");
            File.WriteAllBytes($"fucked-up-{lastFuckedUpId}.bin", data);
            lastFuckedUpId++;
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            var remoteEndPoint = new IPEndPoint(0, 0);
            HandleRawMessage(client.EndReceive(asyncResult, ref remoteEndPoint));
            client.BeginReceive(ReceiveCallback, null);
        }

        public void Close()
        {
            client.Close();
        }

        public void SendMessage(IMessage<object> message)
        {
            lock (this)
            {
                var messageDataWriter = new BinaryWriter(new MemoryStream());
                message.Write(messageDataWriter);
                var messageBytes = ((MemoryStream) messageDataWriter.BaseStream).ToArray();
                var packetsCount = (Math.Max(messageBytes.Length, 1) + MaxSinglePacketSize - 1) / MaxSinglePacketSize;
                var networkMessages = new List<NetworkMessage>();
                for (ushort i = 0; i < packetsCount; i++)
                {
                    networkMessages.Add(new NetworkMessage
                    {
                        MessageId = lastMessageId,
                        FragmentId = i,
                        IsFragmented = packetsCount > 1 || idsToConfirm.Any(),
                        IsLastFragment = i == packetsCount - 1,
                        Type = message.GetMessageType(),
                        MessageDataBytes = messageBytes
                    });
                }

                networkMessages[0].Confirm = idsToConfirm.Select(x => (uint) x).ToArray();
                idsToConfirm.Clear();

                lastMessageId++;
                foreach (var networkMessage in networkMessages)
                {
                    var datagramBytes = networkMessage.GetPacket();
                    client.Send(datagramBytes, datagramBytes.Length);
                }
            }
        }
    }
}
