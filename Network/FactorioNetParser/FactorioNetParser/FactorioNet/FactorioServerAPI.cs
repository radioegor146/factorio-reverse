using System;
using System.Collections.Generic;
using System.IO;
using FactorioNetParser.FactorioNet.Messages;

namespace FactorioNetParser.FactorioNet
{
    public class FactorioServerApi
    {
        private readonly Dictionary<short, FactorioNetMessageBundle> fragmentedPacketsClient =
            new Dictionary<short, FactorioNetMessageBundle>();

        private readonly Dictionary<short, FactorioNetMessageBundle> fragmentedPacketsServer =
            new Dictionary<short, FactorioNetMessageBundle>();

        public void HandlePacketClient(byte[] packet)
        {
            var netmsg = new FactorioNetMessage(packet);
            if (netmsg.IsFragmented)
            {
                if (!fragmentedPacketsClient.ContainsKey(netmsg.MessageId))
                    fragmentedPacketsClient.Add(netmsg.MessageId, new FactorioNetMessageBundle());
                if (!fragmentedPacketsClient[netmsg.MessageId].HandleBundleMessage(netmsg))
                    return;
                HandleMessage((PacketType) netmsg.Type,
                    fragmentedPacketsClient[netmsg.MessageId].GetOverallMessage().PacketBytes, Side.Client);
                fragmentedPacketsClient.Remove(netmsg.MessageId);
            }
            else
            {
                HandleMessage((PacketType) netmsg.Type, netmsg.PacketBytes, Side.Client);
            }
        }

        public void HandlePacketServer(byte[] packet)
        {
            var netmsg = new FactorioNetMessage(packet);
            if (netmsg.IsFragmented)
            {
                if (!fragmentedPacketsServer.ContainsKey(netmsg.MessageId))
                    fragmentedPacketsServer.Add(netmsg.MessageId, new FactorioNetMessageBundle());
                if (!fragmentedPacketsServer[netmsg.MessageId].HandleBundleMessage(netmsg))
                    return;
                HandleMessage((PacketType) netmsg.Type,
                    fragmentedPacketsServer[netmsg.MessageId].GetOverallMessage().PacketBytes, Side.Server);
                fragmentedPacketsServer.Remove(netmsg.MessageId);
            }
            else
            {
                HandleMessage((PacketType) netmsg.Type, netmsg.PacketBytes, Side.Server);
            }
        }

        private static void HandleMessage(PacketType type, byte[] data, Side side)
        {
            switch (type)
            {
                case PacketType.ServerToClientHeartbeat:
                    Program.FactorioPackets.Add(
                        new ServerToClientHeartbeatMessage(new BinaryReader(new MemoryStream(data)))
                    );
                    break;
                case PacketType.ClientToServerHeartbeat:
                    Program.FactorioPackets.Add(
                        new ClientToServerHeartbeatMessage(new BinaryReader(new MemoryStream(data)))
                    );
                    break;
                case PacketType.Ping:
                    break;
                case PacketType.PingReply:
                    break;
                case PacketType.ConnectionRequest:

                    break;
                case PacketType.ConnectionRequestReply:
                    break;
                case PacketType.ConnectionRequestReplyConfirm:
                    break;
                case PacketType.ConnectionAcceptOrDeny:
                    break;
                case PacketType.GetOwnAddress:
                    break;
                case PacketType.GetOwnAddressReply:
                    break;
                case PacketType.NatPunchRequest:
                    break;
                case PacketType.NatPunch:
                    break;
                case PacketType.TransferBlockRequest:
                    break;
                case PacketType.TransferBlock:
                    break;
                case PacketType.RequestForHeartbeatWhenDisconnecting:
                    break;
                case PacketType.LanBroadcast:
                    break;
                case PacketType.GameInformationRequest:
                    break;
                case PacketType.GameInformationRequestReply:
                    break;
                case PacketType.Empty:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}