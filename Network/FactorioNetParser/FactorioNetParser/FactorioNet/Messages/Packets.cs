using System.IO;

namespace FactorioNetParser.FactorioNet.Messages
{
    public interface IPacket
    {
        PacketType GetMessageType();
    }
    public enum PacketType : byte
    {
        ConnectionRequest = 2,
        ConnectionRequestReply = 2,
        ConnectionRequestReplyConfirm = 4,
        ConnectionAcceptOrDeny = 5,
        EmptyPacket = 0x12
    }
    class ConnectionAcceptOrDenyMessage : IReadable<ConnectionAcceptOrDenyMessage>, IPacket
    {
        public bool Online;

        public int ClientrequestId;
        public byte Status;
        public string GameName;
        public string ServerHash;
        public string Description;
        public byte Latency;
        public int GameId;

        public string ServerUsername;
        public byte MapSavingProgress;
        public short var0;
        public Client[] Clients;

        public int FirstSequenceNumberToExpect;
        public int FirstSequenceNumberToSend;
        public short NewPeerId;

        public ModInfo[] Mods;
        public ModSetting[] ModSettings;

        public short PausedBy;

        public int LanGameId;
        public string Name;
        public Version ApplicationVersion;
        public short BuildVersion;
        public string ServerDescription;
        public short MaxPlayers;
        public int GameTimeElapsed;
        public bool HasPassword;
        public string HostAddress;

        public string[] Tags;

        public string ServerUsername1;
        public int AutosaveInterval;
        public int AutosaveSlots;
        public int AFKAutoKickInterval;
        public bool AllowCommands;
        public int MaxUploadInKilobytesPerSecond;
        public byte MinimumLatencyInTicks;
        public bool IgnorePlayerLimitForReturnongPlayers;
        public bool OnlyAdminsCanPauseTheGame;
        public bool RequireUserVerification;

        public string[] Admins;

        public ListItem[] Whitelist;
        public AddressToUsernameMapping[] WhitelistMappings;

        public ListItem[] Banlist;
        public AddressToUsernameMapping[] BanlistMappings;

        public PacketType GetMessageType()
        {
            return PacketType.ConnectionAcceptOrDeny;
        }

        public ConnectionAcceptOrDenyMessage Load(BinaryReader reader)
        {
            ClientrequestId = reader.ReadInt32();
            Status = reader.ReadByte();
            GameName = Reader.ReadString(reader);
            ServerHash = Reader.ReadString(reader);
            Description = Reader.ReadString(reader);
            Latency = reader.ReadByte();
            GameId = reader.ReadInt32();

            ServerUsername = Reader.ReadString(reader);
            MapSavingProgress = reader.ReadByte();
            var0 = reader.ReadVarShort();
            Clients = reader.ReadSimpleArray<Client>();

            FirstSequenceNumberToExpect = reader.ReadInt32();
            FirstSequenceNumberToSend = reader.ReadInt32();
            NewPeerId = reader.ReadInt16();

            Mods = reader.ReadSimpleArray<ModInfo>();
            ModSettings = reader.ReadSimpleArray<ModSetting>();

            PausedBy = reader.ReadInt16();

            LanGameId = reader.ReadInt32();
            Name = Reader.ReadString(reader);
            ApplicationVersion = new Version(reader);
            BuildVersion = reader.ReadInt16();
            ServerDescription = Reader.ReadString(reader);
            MaxPlayers = reader.ReadInt16();
            GameTimeElapsed = reader.ReadInt32();
            HasPassword = reader.ReadBoolean();
            int hostaddrln = reader.ReadInt32();
            byte[] hostaddrstrb = new byte[hostaddrln];
            reader.Read(hostaddrstrb, 0, hostaddrln);
            HostAddress = System.Text.Encoding.UTF8.GetString(hostaddrstrb);

            Tags = reader.ReadArray((x) => Reader.ReadString(x));
            ServerUsername1 = Reader.ReadString(reader);
            AutosaveInterval = reader.ReadInt32();
            AutosaveSlots = reader.ReadInt32();
            AFKAutoKickInterval = reader.ReadInt32();
            AllowCommands = reader.ReadBoolean();
            MaxUploadInKilobytesPerSecond = reader.ReadInt32();
            MinimumLatencyInTicks = reader.ReadByte();
            IgnorePlayerLimitForReturnongPlayers = reader.ReadBoolean();
            OnlyAdminsCanPauseTheGame = reader.ReadBoolean();
            RequireUserVerification = reader.ReadBoolean();

            Admins = reader.ReadArray((x) => Reader.ReadString(x));
            Whitelist = reader.ReadArray<ListItem>();
            WhitelistMappings = reader.ReadArray<AddressToUsernameMapping>();
            Banlist = reader.ReadArray<ListItem>();
            BanlistMappings = reader.ReadArray<AddressToUsernameMapping>();

            Online = true;
            return this;
        }
        public ConnectionAcceptOrDenyMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public ConnectionAcceptOrDenyMessage()
        {
        }
    }
    class ConnectionRequestMessage : IReadable<ConnectionRequestMessage>, IWritable<ConnectionRequestMessage>, IPacket
    {
        public Version Version;
        public short BuildVersion;
        public int ConnectionRequestIDGeneratedOnClient;

        public PacketType GetMessageType()
        {
            return PacketType.ConnectionRequest;
        }

        public ConnectionRequestMessage Load(BinaryReader reader)
        {
            Version = new Version(reader);
            BuildVersion = reader.ReadInt16();
            ConnectionRequestIDGeneratedOnClient = reader.ReadInt32();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            Version.Write(writer);
            writer.Write(BuildVersion);
            writer.Write(ConnectionRequestIDGeneratedOnClient);
        }

        public ConnectionRequestMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public ConnectionRequestMessage()
        {
        }
    }
    class ConnectionRequestReplyConfirmMessage : IReadable<ConnectionRequestReplyConfirmMessage>, IWritable<ConnectionRequestReplyConfirmMessage>, IPacket
    {
        public int ConnectionRequestIDGeneratedOnClient;
        public int ConnectionRequestIDGeneratedOnServer;
        public int InstanceId;
        public string Username = "factoriolauncher";
        public string PasswordHash = "";
        public string ServerKey = "";
        public string ServerKeyTimestamp = "";
        public int CoreChecksum;
        public ModInfo[] Mods = new ModInfo[0];
        public ModSetting[] ModSettings = new ModSetting[0];

        public PacketType GetMessageType()
        {
            return PacketType.ConnectionRequestReplyConfirm;
        }

        public ConnectionRequestReplyConfirmMessage Load(BinaryReader reader)
        {
            ConnectionRequestIDGeneratedOnClient = reader.ReadInt32();
            ConnectionRequestIDGeneratedOnServer = reader.ReadInt32();
            InstanceId = reader.ReadInt32();
            Username = Reader.ReadString(reader);
            PasswordHash = Reader.ReadString(reader);
            ServerKey = Reader.ReadString(reader);
            ServerKeyTimestamp = Reader.ReadString(reader);
            CoreChecksum = reader.ReadInt32();
            Mods = reader.ReadArray<ModInfo>();
            ModSettings = reader.ReadArray<ModSetting>();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(ConnectionRequestIDGeneratedOnClient);
            writer.Write(ConnectionRequestIDGeneratedOnServer);
            writer.Write(InstanceId);
            writer.WriteString(Username);
            writer.WriteString(PasswordHash);
            writer.WriteString(ServerKey);
            writer.WriteString(ServerKeyTimestamp);
            writer.Write(CoreChecksum);
            writer.WriteArray(Mods);
            writer.WriteArray(ModSettings);
        }
        public ConnectionRequestReplyConfirmMessage(BinaryReader reader)
        {
            Load(reader);
        }
        public ConnectionRequestReplyConfirmMessage()
        {
        }
    }
    class ConnectionRequestReplyMessage : IReadable<ConnectionRequestReplyMessage>, IPacket
    {
        public Version Version;
        public short BuildVersion;
        public int ConnectionRequestIDGeneratedOnClient;
        public int ConnectionRequestIDGeneratedOnServer;
        public PacketType GetMessageType()
        {
            return PacketType.ConnectionRequestReply;
        }
        public ConnectionRequestReplyMessage Load(BinaryReader reader)
        {
            Version = new Version(reader);
            BuildVersion = reader.ReadInt16();
            ConnectionRequestIDGeneratedOnClient = reader.ReadInt32();
            ConnectionRequestIDGeneratedOnServer = reader.ReadInt32();
            return this;
        }
    }
    class EmptyMessage : IReadable<EmptyMessage>, IWritable<EmptyMessage>, IPacket
    {
        public PacketType GetMessageType()
        {
            return PacketType.EmptyPacket;
        }

        public EmptyMessage Load(BinaryReader reader)
        {
            return this;
        }

        public void Write(BinaryWriter writer)
        {

        }

        public EmptyMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public EmptyMessage()
        {
        }
    }

    class AddressToUsernameMapping : IReadable<AddressToUsernameMapping>
    {
        public string Username;
        public string Address;
        public AddressToUsernameMapping Load(BinaryReader reader)
        {
            Username = reader.ReadComplexString();
            Address = reader.ReadComplexString();
            return this;
        }
        public AddressToUsernameMapping(BinaryReader reader)
        {
            Load(reader);
        }

        public AddressToUsernameMapping()
        {
        }
    }
    class Client : IReadable<Client>
    {
        public short peerId;
        public string username;
        public byte droppingProgrss;
        public byte mapSavingProgress;
        public byte mapDownloadingProgress;
        public byte mapLoadingProgress;
        public byte tryingToCatchUpProgress;

        public Client Load(BinaryReader reader)
        {
            peerId = Reader.ReadVarShort(reader);
            username = Reader.ReadString(reader);
            byte flags = reader.ReadByte();
            if ((flags & 0x01) > 0)
                droppingProgrss = reader.ReadByte();
            if ((flags & 0x02) > 0)
                mapSavingProgress = reader.ReadByte();
            if ((flags & 0x04) > 0)
                mapDownloadingProgress = reader.ReadByte();
            if ((flags & 0x08) > 0)
                mapLoadingProgress = reader.ReadByte();
            if ((flags & 0x10) > 0)
                tryingToCatchUpProgress = reader.ReadByte();
            return this;
        }
        public Client(BinaryReader reader)
        {
            Load(reader);
        }

        public Client()
        {
        }
    }
    class ListItem : IReadable<ListItem>
    {
        public string Username;
        public string Reason;
        public string Address;
        public ListItem Load(BinaryReader reader)
        {
            Username = Reader.ReadString(reader);
            Reason = Reader.ReadString(reader);
            Address = Reader.ReadString(reader);
            return this;
        }
        public ListItem(BinaryReader reader)
        {
            Load(reader);
        }

        public ListItem()
        {
        }
    }
    class ModInfo : IReadable<ModInfo>, IWritable<ModInfo>
    {
        public string Name;
        public Version Version;
        public int Crc;
        public ModInfo Load(BinaryReader reader)
        {
            Name = Reader.ReadString(reader);
            Version = new Version(reader);
            Crc = reader.ReadInt32();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteString(Name);
            Version.Write(writer);
            writer.Write(Crc);
        }

        public ModInfo(BinaryReader reader)
        {
            Load(reader);
        }

        public ModInfo()
        {
        }
    }
    class ModSetting : IReadable<ModSetting>, IWritable<ModSetting>
    {
        public byte Type;
        public string Name;
        public object Value;

        public ModSetting Load(BinaryReader reader)
        {
            Type = reader.ReadByte();
            switch (Type)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    Name = reader.ReadComplexString();
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
                    Value = reader.ReadComplexString();
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
                    writer.WriteComplexString(Name);
                    break;
                default:
                    throw new IOException("Wrong setting type");
            }
            switch (Type)
            {
                case 1:
                    writer.Write((bool)Value);
                    break;
                case 2:
                    writer.Write((double)Value);
                    break;
                case 3:
                    writer.Write((long)Value);
                    break;
                case 4:
                    writer.WriteComplexString((string)Value);
                    break;
                default:
                    throw new IOException("Wrong setting type");
            }
        }

        public ModSetting(BinaryReader reader)
        {
            Load(reader);
        }
        public ModSetting()
        {
        }
    }
    class Version : IReadable<Version>, IWritable<Version>
    {
        public short MajorVersion;
        public short MinorVersion;
        public short SubVersion;

        public Version Load(BinaryReader reader)
        {
            MajorVersion = reader.ReadVarShort();
            MinorVersion = reader.ReadVarShort();
            SubVersion = reader.ReadVarShort();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteVarShort(MajorVersion);
            writer.WriteVarShort(MinorVersion);
            writer.WriteVarShort(SubVersion);
        }

        public Version(BinaryReader reader)
        {
            Load(reader);
        }

        public Version()
        {
        }
    }
}