using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class CircuitCondition : IReadable<CircuitCondition>, IWritable<CircuitCondition>
    {
        public byte Comparator;
        public SignalId FirstSignalId;
        public SignalId SecondSignalId;
        public int SecondConstant;
        public bool SecondItemIsConstant;

        public CircuitCondition(BinaryReader reader)
        {
            Load(reader);
        }

        public CircuitCondition() { }

        public CircuitCondition Load(BinaryReader reader)
        {
            Comparator = reader.ReadByte();
            FirstSignalId = new SignalId(reader);
            SecondSignalId = new SignalId(reader);
            SecondConstant = reader.ReadInt32();
            SecondItemIsConstant = reader.ReadBoolean();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Comparator);
            FirstSignalId.Write(writer);
            SecondSignalId.Write(writer);
            writer.Write(SecondConstant);
            writer.Write(SecondItemIsConstant);
        }
    }
}