using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class TrainWaitCondition : IReadable<TrainWaitCondition>
    {
        public byte Action;
        public byte AddType;
        public uint ConditionIndex;
        public uint ScheldueIndex;

        public TrainWaitCondition(BinaryReader reader)
        {
            Load(reader);
        }

        public TrainWaitCondition() { }

        public TrainWaitCondition Load(BinaryReader reader)
        {
            Action = reader.ReadByte();
            AddType = reader.ReadByte();
            ScheldueIndex = reader.ReadUInt32();
            ConditionIndex = reader.ReadUInt32();
            return this;
        }
    }
}