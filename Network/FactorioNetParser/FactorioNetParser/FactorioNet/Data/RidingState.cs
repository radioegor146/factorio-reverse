using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class RidingState : IReadable<RidingState>
    {
        public byte AccelerationState;
        public byte Direction;

        public RidingState(BinaryReader reader)
        {
            Load(reader);
        }

        public RidingState() { }

        public RidingState Load(BinaryReader reader)
        {
            Direction = reader.ReadByte();
            AccelerationState = reader.ReadByte();
            return this;
        }
    }
}