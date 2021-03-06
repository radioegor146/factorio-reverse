﻿using System;
using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class TrainWaitConditionData : IReadable<TrainWaitConditionData>, IWritable<TrainWaitConditionData>
    {
        public byte ConditionCompareType;
        public uint ConditionIndex;
        public uint ConditionTicks;
        public byte ConditionType;
        public uint ScheldueIndex;

        public TrainWaitConditionData(BinaryReader reader)
        {
            Load(reader);
        }

        public TrainWaitConditionData() { }

        public TrainWaitConditionData Load(BinaryReader reader)
        {
            ScheldueIndex = reader.ReadUInt32();
            ConditionIndex = reader.ReadUInt32();
            ConditionType = reader.ReadByte();
            if (ConditionType > 9)
                throw new ArgumentOutOfRangeException($"Unknown WaitCondition ConditionType: {ConditionType}");
            ConditionCompareType = reader.ReadByte();
            ConditionTicks = reader.ReadUInt32();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(ScheldueIndex);
            writer.Write(ConditionIndex);
            writer.Write(ConditionType);
            if (ConditionType > 9)
                throw new ArgumentOutOfRangeException($"Unknown WaitCondition ConditionType: {ConditionType}");
            writer.Write(ConditionCompareType);
            writer.Write(ConditionTicks);
        }
    }
}