using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using PcapngUtils.PcapNG;
using FactorioNetParser.FactorioNet;
using FactorioNetParser.FactorioNet.Data;
using FactorioNetParser.FactorioNet.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace FactorioNetParser
{ 
    internal static class Program
    {
        private static readonly Random random = new Random();
        private static uint sequenceNumber;
        private static uint mapSize;
        private static readonly ConcurrentDictionary<uint, byte[]> mapBlocks = new ConcurrentDictionary<uint, byte[]>();
        private static readonly ConcurrentDictionary<uint, uint> notReadyBlocksNumbers = new ConcurrentDictionary<uint, uint>();
        private static bool saved;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            var client = new NetworkClient(new IPEndPoint(Dns.GetHostAddresses(args[0].Split(':')[0]).First(x => x.AddressFamily == AddressFamily.InterNetwork),
                int.Parse(args[0].Split(':').Length == 1 ? "34197" : args[0].Split(':')[1])));
            client.OnMessageReceived += message =>
            {
                switch (message)
                {
                    case ConnectionRequestReplyMessage connectionRequestReplyMessage:
                        client.SendMessage(new ConnectionRequestReplyConfirmMessage
                        {
                            ConnectionRequestIdGeneratedOnClient =
                                connectionRequestReplyMessage.ConnectionRequestIdGeneratedOnClient,
                            ConnectionRequestIdGeneratedOnServer =
                                connectionRequestReplyMessage.ConnectionRequestIdGeneratedOnServer,
                            CoreChecksum = -1891699304,
                            InstanceId = 0,
                            Mods = new []
                            {
                                new ModInfo
                                {
                                    Name = "base",
                                    Crc = -346953843,
                                    Version = connectionRequestReplyMessage.Version
                                }
                            },
                            ModSettings = new ModSetting[0],
                            PasswordHash = "",
                            ServerKey = "",
                            ServerKeyTimestamp = "",
                            Username = "radioegor146"
                        });
                        break;
                    case ConnectionAcceptOrDenyMessage connectionAcceptOrDenyMessage:
                        sequenceNumber = connectionAcceptOrDenyMessage.FirstSequenceNumberToSend;
                        client.SendMessage(new EmptyMessage());
                        logger.Info("Connected");
                        break;
                    case ServerToClientHeartbeatMessage serverToClientHeartbeatMessage:
                        if (serverToClientHeartbeatMessage.SynchronizerActions != null &&
                            serverToClientHeartbeatMessage.SynchronizerActions.Length > 0)
                            foreach (var syncronizerAction in serverToClientHeartbeatMessage.SynchronizerActions)
                            {
                                // ReSharper disable once SwitchStatementMissingSomeCases
                                switch (syncronizerAction.Item1.Action)
                                {
                                    case SynchronizerActionType.GameEnd:
                                        break;
                                    case SynchronizerActionType.PeerDisconnect:
                                        break;
                                    case SynchronizerActionType.NewPeerInfo:
                                        break;
                                    case SynchronizerActionType.MapReadyForDownload:
                                        mapSize = (uint) syncronizerAction.Item1.Data[0];
                                        var blocks = (mapSize + 502) / 503;
                                        logger.Info($"Map ready - {mapSize} bytes/{blocks} blocks");
                                        for (uint i = 0; i < blocks; i++)
                                            notReadyBlocksNumbers.TryAdd(i, i);
                                        new Thread(() =>
                                        {
                                            while (notReadyBlocksNumbers.Any())
                                            {
                                                var currentBlockNumbers = notReadyBlocksNumbers.Take(50);
                                                foreach (var blockNumber in currentBlockNumbers)
                                                {
                                                    client.SendMessage(new TransferBlockRequestMessage
                                                    {
                                                        BlockNumber = blockNumber.Key
                                                    });
                                                }

                                                Thread.Sleep(10);
                                            }
                                        }).Start();
                                        new Thread(() =>
                                        {
                                            while (mapBlocks.Count != blocks)
                                            {
                                                logger.Debug($"Map downloading: {1.0 * mapBlocks.Count / blocks * 100:0.0}%");
                                                Thread.Sleep(1000);
                                            }
                                        }).Start();
                                        break;
                                    case SynchronizerActionType.MapSavingProgressUpdate:
                                        logger.Info(
                                            $"Map server saving - {1.0 * (byte) syncronizerAction.Item1.Data[0] / 255 * 100:0.0}%");
                                        break;
                                    default:
                                        //Console.WriteLine(syncronizerAction.Item1.Action);
                                        break;
                                }
                            }

                        //var inputActions = new List<InputAction>();
                        //if (serverToClientHeartbeatMessage.TickClosures != null)
                        //    foreach (var tickClosure in serverToClientHeartbeatMessage.TickClosures)
                        //        if (tickClosure.Item2?.InputActions != null)
                        //            inputActions.AddRange(tickClosure.Item2.InputActions);

                        if (serverToClientHeartbeatMessage.RequestsForHeartbeat != null &&
                            serverToClientHeartbeatMessage.RequestsForHeartbeat.Length > 0)
                        {
                            foreach (var requestedSequenceNumber in serverToClientHeartbeatMessage.RequestsForHeartbeat)
                            {
                                client.SendMessage(new ClientToServerHeartbeatMessage
                                {
                                    NextReceiveServerTickClosure = uint.MaxValue,
                                    SequenceNumber = requestedSequenceNumber
                                });
                            }
                        }
                        sequenceNumber++;
                        client.SendMessage(new ClientToServerHeartbeatMessage
                        {
                            NextReceiveServerTickClosure = uint.MaxValue,
                            SequenceNumber = sequenceNumber
                        });
                        break;
                    case TransferBlockMessage transferBlockMessage:
                        {
                            if (!notReadyBlocksNumbers.ContainsKey(transferBlockMessage.BlockNumber))
                                break;
                            mapBlocks.TryAdd(transferBlockMessage.BlockNumber, transferBlockMessage.Data);
                            notReadyBlocksNumbers.TryRemove(transferBlockMessage.BlockNumber, out _);
                            var fragments = (mapSize + 502) / 503;
                            if (mapBlocks.Count == fragments)
                            {
                                var mapFileStream = new FileStream($"map-{args[0].Replace(':', '_')}.zip", FileMode.Create);
                                for (uint i = 0; i < fragments; i++)
                                    if (i == fragments - 1)
                                        mapFileStream.Write(mapBlocks[i], 0, (int) (mapSize % 503));
                                    else
                                        mapFileStream.Write(mapBlocks[i], 0, 503);
                                mapFileStream.Close();
                                logger.Info($"Saved to map-{args[0].Replace(':', '_')}.zip");
                                saved = true;
                            }
                        }
                    break;
                }

            };
            client.Open();
            client.SendMessage(new ConnectionRequestMessage
            {
                ConnectionRequestIdGeneratedOnClient = random.Next(),
                Version = new FactorioNet.Data.Version
                {
                    MajorVersion = 0,
                    MinorVersion = 17,
                    SubVersion = 66
                },
                BuildVersion = 46866
            });
            logger.Info("Connecting...");
            SpinWait.SpinUntil(() => saved);
        }
    }

    
}
