using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class ArithmeticCombinatorParameters : IReadable<ArithmeticCombinatorParameters>
    {
        public int FirstConstant;
        public SignalId FirstSignalId;
        public bool FirstSignalIsConstant;
        public byte Operation;
        public SignalId OutputSignalId;
        public int SecondConstant;
        public SignalId SecondSignalId;
        public bool SecondSignalIsConstant;

        public ArithmeticCombinatorParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public ArithmeticCombinatorParameters() { }

        public ArithmeticCombinatorParameters Load(BinaryReader reader)
        {
            FirstSignalId = new SignalId(reader);
            SecondSignalId = new SignalId(reader);
            OutputSignalId = new SignalId(reader);
            SecondConstant = reader.ReadInt32();
            Operation = reader.ReadByte();
            SecondSignalIsConstant = reader.ReadBoolean();
            FirstConstant = reader.ReadInt32();
            FirstSignalIsConstant = reader.ReadBoolean();
            return this;
        }
    }
}