using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class ArithmeticCombinatorParameters : IReadable<ArithmeticCombinatorParameters>, IWritable<ArithmeticCombinatorParameters>
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

        public void Write(BinaryWriter writer)
        {
            FirstSignalId.Write(writer);
            SecondSignalId.Write(writer);
            OutputSignalId.Write(writer);
            writer.Write(SecondConstant);
            writer.Write(Operation);
            writer.Write(SecondSignalIsConstant);
            writer.Write(FirstConstant);
            writer.Write(FirstSignalIsConstant);
        }
    }
}