using System;
using System.Collections.Generic;
using System.IO;
using TempAApp.FactorioNet.Messages;

namespace TempAApp.FactorioNet
{
    public class FactorioServerApi
    {
        private readonly Dictionary<short, FactorioNetMessageBundle> fragmentedPacketsClient = new Dictionary<short, FactorioNetMessageBundle>();
        private readonly Dictionary<short, FactorioNetMessageBundle> fragmentedPacketsServer = new Dictionary<short, FactorioNetMessageBundle>();
        
        public void HandlePacketClient(byte[] packet)
        {
            var netmsg = new FactorioNetMessage(packet);
            if(netmsg.IsFragmented)
            {
                if(!fragmentedPacketsClient.ContainsKey(netmsg.MessageId))
                {
                    fragmentedPacketsClient.Add(netmsg.MessageId, new FactorioNetMessageBundle());
                }
                if (!fragmentedPacketsClient[netmsg.MessageId].HandleBundleMessage(netmsg))
                    return;
                HandleMessage((PacketType)netmsg.Type, fragmentedPacketsClient[netmsg.MessageId].GetOverallMessage().PacketBytes, Side.Client);
                fragmentedPacketsClient.Remove(netmsg.MessageId);
            }
            else
            {
                HandleMessage((PacketType)netmsg.Type, netmsg.PacketBytes, Side.Client);
            }
        }

        public void HandlePacketServer(byte[] packet)
        {
            var netmsg = new FactorioNetMessage(packet);
            if (netmsg.IsFragmented)
            {
                if (!fragmentedPacketsServer.ContainsKey(netmsg.MessageId))
                {
                    fragmentedPacketsServer.Add(netmsg.MessageId, new FactorioNetMessageBundle());
                }
                if (!fragmentedPacketsServer[netmsg.MessageId].HandleBundleMessage(netmsg))
                    return;
                HandleMessage((PacketType)netmsg.Type, fragmentedPacketsServer[netmsg.MessageId].GetOverallMessage().PacketBytes, Side.Server);
                fragmentedPacketsServer.Remove(netmsg.MessageId);
            }
            else
            {
                HandleMessage((PacketType)netmsg.Type, netmsg.PacketBytes, Side.Server);
            }
        }

        private static void HandleMessage(PacketType type, byte[] data, Side side)
        {
            if (type == PacketType.ServerToClientHeartbeat)
                new ServerToClientHeartbeatMessage(new BinaryReader(new MemoryStream(data)));
            //Console.WriteLine($"Got packet {type} from {side} with size of {data.Length}");
        }
    }
}
