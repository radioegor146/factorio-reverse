using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class DeciderCombinatorParameters : IReadable<DeciderCombinatorParameters>
    {
        public byte Comparator;
        public bool CopyCountFromInput;
        public SignalId FirstSignalId;
        public bool SecondSignalIsConstant;
        public SignalId OutputSignalId;
        public int SecondConstant;
        public SignalId SecondSignalId;

        public DeciderCombinatorParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public DeciderCombinatorParameters() { }

        public DeciderCombinatorParameters Load(BinaryReader reader)
        {
            FirstSignalId = new SignalId(reader);
            SecondSignalId = new SignalId(reader);
            OutputSignalId = new SignalId(reader);
            SecondConstant = reader.ReadInt32();
            Comparator = reader.ReadByte();
            CopyCountFromInput = reader.ReadBoolean();
            SecondSignalIsConstant = reader.ReadBoolean();
            return this;
        }
    }
}