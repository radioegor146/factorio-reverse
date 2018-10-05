using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using PcapngUtils.Pcap;
using PcapngUtils.PcapNG;
using FactorioNetParser.FactorioNet;
using FactorioNetParser.FactorioNet.Messages;
using Newtonsoft.Json;

namespace FactorioNetParser
{

    internal class Program
    {
        public static List<IPacket> FactorioPackets = new List<IPacket>();

        private static readonly FactorioServerApi serverApi = new FactorioServerApi();

        private static void Main(string[] args)
        {
            using (var reader = new PcapNGReader("factorio.pcapng", false))
            {
                reader.OnReadPacketEvent += Reader_OnReadPacketEvent;
                reader.ReadPackets(new CancellationToken());
            }

            File.WriteAllText("packets.json", JsonConvert.SerializeObject(FactorioPackets));
        }

        private static void Reader_OnReadPacketEvent(object context, PcapngUtils.Common.IPacket packet)
        {
            if (packet.Data[0] == 0x2c)
                serverApi.HandlePacketClient(packet.Data.Skip(42).ToArray());
            else
                serverApi.HandlePacketServer(packet.Data.Skip(42).ToArray());
        }
    }

    public enum Side
    {
        Client,
        Server
    }

    public enum PacketType
    {
        Ping,
        PingReply,
        ConnectionRequest,
        ConnectionRequestReply,
        ConnectionRequestReplyConfirm,
        ConnectionAcceptOrDeny,
        ClientToServerHeartbeat,
        ServerToClientHeartbeat,
        GetOwnAddress,
        GetOwnAddressReply,
        NatPunchRequest,
        NatPunch,
        TransferBlockRequest,
        TransferBlock,
        RequestForHeartbeatWhenDisconnecting,
        LanBroadcast,
        GameInformationRequest,
        GameInformationRequestReply,
        Empty,
    }
}
