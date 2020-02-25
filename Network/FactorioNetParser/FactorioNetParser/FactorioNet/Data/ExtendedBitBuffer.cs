using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class ExtendedBitBuffer : IReadable<ExtendedBitBuffer>, IWritable<ExtendedBitBuffer>
    {
        public uint Bits;
        public uint[] Data;

        public ExtendedBitBuffer(BinaryReader reader)
        {
            Load(reader);
        }

        public ExtendedBitBuffer() { }

        public ExtendedBitBuffer Load(BinaryReader reader)
        {
            var tValue = reader.ReadVarInt();
            Data = new uint[tValue >> 5];
            for (var i = 0; i < Data.Length; i++)
                Data[i] = reader.ReadUInt32();
            if ((tValue & 0x1F) > 0)
                Bits = reader.ReadUInt32();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            var tValue = Data.Length << 5;
            if (Bits > 0)
                tValue |= 0x1F;
            writer.Write(tValue);
            for (var i = 0; i < Data.Length; i++)
                writer.Write(Data[i]);
            if (Bits > 0)
            {
                writer.Write(Bits);
            }
        }
    }
}