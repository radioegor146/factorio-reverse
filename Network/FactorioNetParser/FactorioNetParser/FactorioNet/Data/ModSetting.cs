using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class ModSetting : IReadable<ModSetting>, IWritable<ModSetting>
    {
        public string Name;
        public byte Type;
        public object Value;

        public ModSetting(BinaryReader reader)
        {
            Load(reader);
        }

        public ModSetting() { }

        public ModSetting Load(BinaryReader reader)
        {
            Type = reader.ReadByte();
            switch (Type)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    Name = reader.ReadFactorioString();
                    break;
                default:
                    throw new IOException("Wrong setting type");
            }

            switch (Type)
            {
                case 1:
                    Value = reader.ReadBoolean();
                    break;
                case 2:
                    Value = reader.ReadDouble();
                    break;
                case 3:
                    Value = reader.ReadInt64();
                    break;
                case 4:
                    Value = reader.ReadFactorioString();
                    break;
                default:
                    throw new IOException("Wrong setting type");
            }

            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Type);
            switch (Type)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    writer.WriteString(Name);
                    break;
                default:
                    throw new IOException("Wrong setting type");
            }

            switch (Type)
            {
                case 1:
                    writer.Write((bool) Value);
                    break;
                case 2:
                    writer.Write((double) Value);
                    break;
                case 3:
                    writer.Write((long) Value);
                    break;
                case 4:
                    writer.WriteString((string) Value);
                    break;
                default:
                    throw new IOException("Wrong setting type");
            }
        }
    }
}