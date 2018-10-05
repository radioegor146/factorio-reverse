using System.IO;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class Class0 : IReadable<Class0>
    {
        public short var0;
        public int var1;

        public Class0(BinaryReader reader)
        {
            Load(reader);
        }

        public Class0() { }

        public Class0 Load(BinaryReader reader)
        {
            var0 = reader.ReadInt16();
            var1 = reader.ReadInt32();
            return this;
        }
    }

    internal class Class1 : IReadable<Class1>
    {
        public byte var0;
        public int var1;

        public Class1(BinaryReader reader)
        {
            Load(reader);
        }

        public Class1() { }

        public Class1 Load(BinaryReader reader)
        {
            var0 = reader.ReadByte();
            var1 = reader.ReadInt32();
            return this;
        }
    }
}