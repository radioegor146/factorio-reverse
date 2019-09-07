using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class SynchronizerAction : IReadable<SynchronizerAction>, IWritable<SynchronizerAction>
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public SynchronizerActionType Action;

        public List<object> Data = new List<object>();

        public SynchronizerAction() { }

        public SynchronizerAction(BinaryReader reader)
        {
            Load(reader);
        }

        public SynchronizerAction Load(BinaryReader reader)
        {
            Action = (SynchronizerActionType) reader.ReadByte();
            switch (Action)
            {
                case SynchronizerActionType.GameEnd:
                case SynchronizerActionType.SavingForUpdate:
                case SynchronizerActionType.PlayerDesynced:
                case SynchronizerActionType.BeginPause:
                case SynchronizerActionType.EndPause:
                case SynchronizerActionType.InputActionSegmentsInFlightFinished:
                    break;
                case SynchronizerActionType.PeerDisconnect:
                case SynchronizerActionType.ClientChangedState:
                case SynchronizerActionType.MapLoadingProgressUpdate:
                case SynchronizerActionType.MapSavingProgressUpdate:
                case SynchronizerActionType.MapDownloadingProgressUpdate:
                case SynchronizerActionType.ChangeLatency:
                case SynchronizerActionType.CatchingUpProgressUpdate:
                case SynchronizerActionType.PeerDroppingProgressUpdate:
                    Add(reader.ReadByte());
                    break;
                case SynchronizerActionType.NewPeerInfo:
                    Add(reader.ReadFactorioString());
                    break;
                case SynchronizerActionType.ClientShouldStartSendingTickClosures:
                case SynchronizerActionType.SkippedTickClosure:
                case SynchronizerActionType.SkippedTickClosureConfirm:
                case SynchronizerActionType.SavingCountDown:
                    Add(reader.ReadUInt32());
                    break;
                case SynchronizerActionType.MapReadyForDownload:
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt32());
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    Add(reader.ReadArray(x => Tuple.Create(reader.ReadFactorioString(), reader.ReadUInt32()))
                        .ToDictionary(x => x.Item1, y => y.Item2));
                    Add(reader.ReadArray(x => Tuple.Create(reader.ReadFactorioString(), new ScriptRegistrations(reader)))
                        .ToDictionary(x => x.Item1, y => y.Item2));
                    Add(reader.ReadArray(x =>
                            Tuple.Create(reader.ReadFactorioString(), reader.ReadArray(y => y.ReadFactorioString())))
                        .ToDictionary(x => x.Item1, y => y.Item2));
                    break;
                case SynchronizerActionType.IncreasedLatencyConfirm:
                    Add(reader.ReadUInt32());
                    Add(reader.ReadByte());
                    break;
                case SynchronizerActionType.InputActionSegmentsInFlight:
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt32());
                    break;
                default:
                    throw new Exception($"No such SynchronizerAction: {Action}");
            }

            return this;
        }

        public void Add(object a)
        {
            Data.Add(a);
        }

        public void Write(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}