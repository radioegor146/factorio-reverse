using System.IO;
using System.Text;

namespace TempAApp.FactorioNet
{
    public static class Reader
    {
        public static short ReadVarShort(this BinaryReader stream)
        {
            int ansval;
            if ((ansval = (int)(stream.ReadByte() & 0xFF)) == 255)
                ansval = stream.ReadInt16();
            return (short)ansval;
        }
        public static void WriteVarShort(this BinaryWriter stream, short data)
        {
            if (data > 0xFF)
            {
                stream.Write((byte)(0xFF));
                stream.Write(data);
            }
            else
            {
                stream.Write((byte)(data));
            }
        }
        public static int ReadVarInt(this BinaryReader stream)
        {
            int ansval;
            if ((ansval = (int)(stream.ReadByte() & 0xFF)) == 255)
                ansval = stream.ReadInt32();
            return ansval;
        }
        public static void WriteVarInt(this BinaryWriter stream, int data)
        {
            if (data > 0xFF)
            {
                stream.Write((byte)(0xFF));
                stream.Write(data);
            }
            else
            {
                stream.Write((byte)data);
            }
        }
        public static string ReadString(this BinaryReader stream)
        {
            short strln = ReadVarShort(stream);
            byte[] data = new byte[strln];
            stream.Read(data, 0, strln);
            return Encoding.UTF8.GetString(data);
        }
        public static void WriteString(this BinaryWriter stream, string data)
        {
            byte[] dt = Encoding.UTF8.GetBytes(data);
            WriteVarShort(stream, (short)dt.Length);
            stream.Write(dt);
        }
        public static string ReadComplexString(this BinaryReader stream)
        {
            int strln = ReadVarInt(stream);
            byte[] data = new byte[strln];
            stream.Read(data, 0, strln);
            return Encoding.UTF8.GetString(data);
        }
        public static void WriteComplexString(this BinaryWriter stream, string data)
        {
            byte[] dt = Encoding.UTF8.GetBytes(data);
            WriteVarInt(stream, dt.Length);
            stream.Write(dt);
        }

        public static T[] ReadArray<T> (this BinaryReader stream) where T : IReadable<T>, new()
        {
            T[] array = new T[stream.ReadVarInt()];
            for (int i = 0; i < array.Length; i++)
                array[i] = new T().Load(stream);
            return array;
        }
        public delegate T GetFromStream<T>(BinaryReader reader);
        public static T[] ReadArray<T> (this BinaryReader stream, GetFromStream<T> read)
        {
            T[] array = new T[stream.ReadVarInt()];
            for (int i = 0; i < array.Length; i++)
                array[i] = read(stream);
            return array;
        }

    }
}
