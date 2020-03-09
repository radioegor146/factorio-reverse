using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class Client : IReadable<Client>, IWritable<Client>
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

        public void Write(BinaryWriter writer)
        {
            writer.WriteVarShort(PeerId);
            writer.WriteSimpleString(Username);
            var flags = (byte)((DroppingProgrss > 0 ? 0x01 : 0)
                | (MapSavingProgress > 0 ? 0x02 : 0)
                | (MapDownloadingProgress > 0 ? 0x04 : 0)
                | (MapLoadingProgress > 0 ? 0x08 : 0)
                | (TryingToCatchUpProgress > 0 ? 0x10 : 0));
            writer.Write(flags);
            if (DroppingProgrss > 0)
                writer.Write(DroppingProgrss);
            if (MapSavingProgress > 0)
                writer.Write(MapSavingProgress);
            if (MapDownloadingProgress > 0)
                writer.Write(MapDownloadingProgress);
            if (MapLoadingProgress > 0)
                writer.Write(MapLoadingProgress);
            if (TryingToCatchUpProgress > 0)
                writer.Write(TryingToCatchUpProgress);
        }
    }
}