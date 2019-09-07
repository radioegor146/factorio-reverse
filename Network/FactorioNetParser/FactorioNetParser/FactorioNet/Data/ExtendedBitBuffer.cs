using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class ExtendedBitBuffer : IReadable<ExtendedBitBuffer>
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
    }
}