using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class Client : IReadable<Client>
    {
        public byte DroppingProgrss;
        public byte MapDownloadingProgress;
        public byte MapLoadingProgress;
        public byte MapSavingProgress;
        public short PeerId;
        public byte TryingToCatchUpProgress;
        public string Username;

        public Client(BinaryReader reader)
        {
            Load(reader);
        }

        public Client() { }

        public Client Load(BinaryReader reader)
        {
            PeerId = reader.ReadVarShort();
            Username = reader.ReadSimpleString();
            var flags = reader.ReadByte();
            if ((flags & 0x01) > 0)
                DroppingProgrss = reader.ReadByte();
            if ((flags & 0x02) > 0)
                MapSavingProgress = reader.ReadByte();
            if ((flags & 0x04) > 0)
                MapDownloadingProgress = reader.ReadByte();
            if ((flags & 0x08) > 0)
                MapLoadingProgress = reader.ReadByte();
            if ((flags & 0x10) > 0)
                TryingToCatchUpProgress = reader.ReadByte();
            return this;
        }
    }
}