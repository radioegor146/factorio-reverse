using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class BuildRailData : IReadable<BuildRailData>, IWritable<BuildRailData>
    {
        public bool AlternativeBuild;
        public ExtendedBitBuffer Buffer;
        public Direction Direction;
        public byte Mode;
        public int StartX;
        public int StartY;

        public BuildRailData(BinaryReader reader)
        {
            Load(reader);
        }

        public BuildRailData() { }

        public BuildRailData Load(BinaryReader reader)
        {
            Mode = reader.ReadByte();
            StartX = reader.ReadInt32();
            StartY = reader.ReadInt32();
            Direction = new Direction(reader);
            Buffer = new ExtendedBitBuffer(reader);
            AlternativeBuild = reader.ReadBoolean();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Mode);
            writer.Write(StartX);
            writer.Write(StartY);
            Direction.Write(writer);
            Buffer.Write(writer);
            writer.Write(AlternativeBuild);
        }
    }
}