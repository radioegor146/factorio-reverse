using System.IO;
using System.Text;

namespace FactorioNetParser.FactorioNet
{
    public static class Reader
    {
        public static short ReadVarShort(this BinaryReader stream)
        {
            int ansval;
            if ((ansval = stream.ReadByte() & 0xFF) == 255)
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
            if ((ansval = stream.ReadByte() & 0xFF) == 255)
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
            var strln = ReadVarShort(stream);
            var data = new byte[strln];
            stream.Read(data, 0, strln);
            return Encoding.UTF8.GetString(data);
        }

        public static void WriteString(this BinaryWriter stream, string data)
        {
            var dt = Encoding.UTF8.GetBytes(data);
            WriteVarShort(stream, (short)dt.Length);
            stream.Write(dt);
        }

        public static string ReadComplexString(this BinaryReader stream)
        {
            var strln = ReadVarInt(stream);
            var data = new byte[strln];
            stream.Read(data, 0, strln);
            return Encoding.UTF8.GetString(data);
        }

        public static void WriteComplexString(this BinaryWriter stream, string data)
        {
            var dt = Encoding.UTF8.GetBytes(data);
            WriteVarInt(stream, dt.Length);
            stream.Write(dt);
        }

        public static T[] ReadArray<T> (this BinaryReader stream) where T : IReadable<T>, new()
        {
            var array = new T[stream.ReadVarInt()];
            for (var i = 0; i < array.Length; i++)
                array[i] = new T().Load(stream);
            return array;
        }

        public delegate T GetFromStream<out T>(BinaryReader reader);

        public static T[] ReadArray<T> (this BinaryReader stream, GetFromStream<T> read)
        {
            var array = new T[stream.ReadVarInt()];
            for (var i = 0; i < array.Length; i++)
                array[i] = read(stream);
            return array;
        }

        public static T[] ReadSimpleArray<T>(this BinaryReader stream) where T : IReadable<T>, new()
        {
            var array = new T[stream.ReadVarShort()];
            for (var i = 0; i < array.Length; i++)
                array[i] = new T().Load(stream);
            return array;
        }

        public static T[] ReadSimpleArray<T>(this BinaryReader stream, GetFromStream<T> read)
        {
            var array = new T[stream.ReadVarShort()];
            for (var i = 0; i < array.Length; i++)
                array[i] = read(stream);
            return array;
        }

        public delegate void SetToStream<in T>(BinaryWriter reader, T o);
        public static void WriteArray<T>(this BinaryWriter stream, T[] data) where T : IWritable<T>
        {
            stream.WriteVarInt(data.Length);
            for (int i = 0; i < data.Length; i++)
                data[i].Write(stream);
        }
        public static void WriteArray<T>(this BinaryWriter stream, T[] data, SetToStream<T> write)
        {
            stream.WriteVarInt(data.Length);
            for (int i = 0; i < data.Length; i++)
                write(stream, data[i]);
        }
        public static void WriteSimpleArray<T>(this BinaryWriter stream, T[] data) where T : IWritable<T>
        {
            stream.WriteVarShort((short)data.Length);
            for (int i = 0; i < data.Length; i++)
                data[i].Write(stream);
        }
        public static void WriteSimpleArray<T>(this BinaryWriter stream, T[] data, SetToStream<T> write)
        {
            stream.WriteVarShort((short)data.Length);
            for (int i = 0; i < data.Length; i++)
                write(stream, data[i]);
        }
    }
}
