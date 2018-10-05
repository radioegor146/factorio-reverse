using System.IO;
using System.Text;

namespace FactorioNetParser.FactorioNet.Messages
{
    public interface IPacket
    {
        PacketType GetMessageType();
    }

    internal class ConnectionAcceptOrDenyMessage : IReadable<ConnectionAcceptOrDenyMessage>, IPacket
    {
        public string[] Admins;
        public int AfkAutoKickInterval;
        public bool AllowCommands;
        public Version ApplicationVersion;
        public int AutosaveInterval;
        public int AutosaveSlots;

        public ListItem[] Banlist;
        public AddressToUsernameMapping[] BanlistMappings;
        public short BuildVersion;

        public int ClientrequestId;
        public Client[] Clients;
        public string Description;

        public int FirstSequenceNumberToExpect;
        public int FirstSequenceNumberToSend;
        public int GameId;
        public string GameName;
        public int GameTimeElapsed;
        public bool HasPassword;
        public string HostAddress;
        public bool IgnorePlayerLimitForReturnongPlayers;

        public int LanGameId;
        public byte Latency;
        public byte MapSavingProgress;
        public short MaxPlayers;
        public int MaxUploadInKilobytesPerSecond;
        public byte MinimumLatencyInTicks;

        public ModInfo[] Mods;
        public ModSetting[] ModSettings;
        public string Name;
        public short NewPeerId;
        public bool Online;
        public bool OnlyAdminsCanPauseTheGame;

        public short PausedBy;
        public bool RequireUserVerification;
        public string ServerDescription;
        public string ServerHash;

        public string ServerUsername;

        public string ServerUsername1;
        public byte Status;

        public string[] Tags;
        public short var0;

        public ListItem[] Whitelist;
        public AddressToUsernameMapping[] WhitelistMappings;

        public ConnectionAcceptOrDenyMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public ConnectionAcceptOrDenyMessage() { }

        public PacketType GetMessageType()
        {
            return PacketType.ConnectionAcceptOrDeny;
        }

        public ConnectionAcceptOrDenyMessage Load(BinaryReader reader)
        {
            ClientrequestId = reader.ReadInt32();
            Status = reader.ReadByte();
            GameName = reader.ReadSimpleString();
            ServerHash = reader.ReadSimpleString();
            Description = reader.ReadSimpleString();
            Latency = reader.ReadByte();
            GameId = reader.ReadInt32();

            ServerUsername = reader.ReadSimpleString();
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
            Name = reader.ReadSimpleString();
            ApplicationVersion = new Version(reader);
            BuildVersion = reader.ReadInt16();
            ServerDescription = reader.ReadSimpleString();
            MaxPlayers = reader.ReadInt16();
            GameTimeElapsed = reader.ReadInt32();
            HasPassword = reader.ReadBoolean();
            var hostaddrln = reader.ReadInt32();
            var hostaddrstrb = new byte[hostaddrln];
            reader.Read(hostaddrstrb, 0, hostaddrln);
            HostAddress = Encoding.UTF8.GetString(hostaddrstrb);

            Tags = reader.ReadArray(Reader.ReadSimpleString);
            ServerUsername1 = reader.ReadSimpleString();
            AutosaveInterval = reader.ReadInt32();
            AutosaveSlots = reader.ReadInt32();
            AfkAutoKickInterval = reader.ReadInt32();
            AllowCommands = reader.ReadBoolean();
            MaxUploadInKilobytesPerSecond = reader.ReadInt32();
            MinimumLatencyInTicks = reader.ReadByte();
            IgnorePlayerLimitForReturnongPlayers = reader.ReadBoolean();
            OnlyAdminsCanPauseTheGame = reader.ReadBoolean();
            RequireUserVerification = reader.ReadBoolean();

            Admins = reader.ReadArray(Reader.ReadSimpleString);
            Whitelist = reader.ReadArray<ListItem>();
            WhitelistMappings = reader.ReadArray<AddressToUsernameMapping>();
            Banlist = reader.ReadArray<ListItem>();
            BanlistMappings = reader.ReadArray<AddressToUsernameMapping>();

            Online = true;
            return this;
        }
    }

    internal class ConnectionRequestMessage : IReadable<ConnectionRequestMessage>, IWritable<ConnectionRequestMessage>,
        IPacket
    {
        public short BuildVersion;
        public int ConnectionRequestIdGeneratedOnClient;
        public Version Version;

        public ConnectionRequestMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public ConnectionRequestMessage() { }

        public PacketType GetMessageType()
        {
            return PacketType.ConnectionRequest;
        }

        public ConnectionRequestMessage Load(BinaryReader reader)
        {
            Version = new Version(reader);
            BuildVersion = reader.ReadInt16();
            ConnectionRequestIdGeneratedOnClient = reader.ReadInt32();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            Version.Write(writer);
            writer.Write(BuildVersion);
            writer.Write(ConnectionRequestIdGeneratedOnClient);
        }
    }

    internal class ConnectionRequestReplyConfirmMessage : IReadable<ConnectionRequestReplyConfirmMessage>,
        IWritable<ConnectionRequestReplyConfirmMessage>, IPacket
    {
        public int ConnectionRequestIdGeneratedOnClient;
        public int ConnectionRequestIdGeneratedOnServer;
        public int CoreChecksum;
        public int InstanceId;
        public ModInfo[] Mods = new ModInfo[0];
        public ModSetting[] ModSettings = new ModSetting[0];
        public string PasswordHash = "";
        public string ServerKey = "";
        public string ServerKeyTimestamp = "";
        public string Username = "factoriolauncher";

        public ConnectionRequestReplyConfirmMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public ConnectionRequestReplyConfirmMessage() { }

        public PacketType GetMessageType()
        {
            return PacketType.ConnectionRequestReplyConfirm;
        }

        public ConnectionRequestReplyConfirmMessage Load(BinaryReader reader)
        {
            ConnectionRequestIdGeneratedOnClient = reader.ReadInt32();
            ConnectionRequestIdGeneratedOnServer = reader.ReadInt32();
            InstanceId = reader.ReadInt32();
            Username = reader.ReadSimpleString();
            PasswordHash = reader.ReadSimpleString();
            ServerKey = reader.ReadSimpleString();
            ServerKeyTimestamp = reader.ReadSimpleString();
            CoreChecksum = reader.ReadInt32();
            Mods = reader.ReadArray<ModInfo>();
            ModSettings = reader.ReadArray<ModSetting>();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(ConnectionRequestIdGeneratedOnClient);
            writer.Write(ConnectionRequestIdGeneratedOnServer);
            writer.Write(InstanceId);
            writer.WriteSimpleString(Username);
            writer.WriteSimpleString(PasswordHash);
            writer.WriteSimpleString(ServerKey);
            writer.WriteSimpleString(ServerKeyTimestamp);
            writer.Write(CoreChecksum);
            writer.WriteArray(Mods);
            writer.WriteArray(ModSettings);
        }
    }

    internal class ConnectionRequestReplyMessage : IReadable<ConnectionRequestReplyMessage>, IPacket
    {
        public short BuildVersion;
        public int ConnectionRequestIdGeneratedOnClient;
        public int ConnectionRequestIdGeneratedOnServer;
        public Version Version;

        public PacketType GetMessageType()
        {
            return PacketType.ConnectionRequestReply;
        }

        public ConnectionRequestReplyMessage Load(BinaryReader reader)
        {
            Version = new Version(reader);
            BuildVersion = reader.ReadInt16();
            ConnectionRequestIdGeneratedOnClient = reader.ReadInt32();
            ConnectionRequestIdGeneratedOnServer = reader.ReadInt32();
            return this;
        }
    }

    internal class EmptyMessage : IReadable<EmptyMessage>, IWritable<EmptyMessage>, IPacket
    {
        public EmptyMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public EmptyMessage() { }

        public PacketType GetMessageType()
        {
            return PacketType.Empty;
        }

        public EmptyMessage Load(BinaryReader reader)
        {
            return this;
        }

        public void Write(BinaryWriter writer) { }
    }

    internal class AddressToUsernameMapping : IReadable<AddressToUsernameMapping>
    {
        public string Address;
        public string Username;

        public AddressToUsernameMapping(BinaryReader reader)
        {
            Load(reader);
        }

        public AddressToUsernameMapping() { }

        public AddressToUsernameMapping Load(BinaryReader reader)
        {
            Username = reader.ReadFactorioString();
            Address = reader.ReadFactorioString();
            return this;
        }
    }

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

    internal class ListItem : IReadable<ListItem>
    {
        public string Address;
        public string Reason;
        public string Username;

        public ListItem(BinaryReader reader)
        {
            Load(reader);
        }

        public ListItem() { }

        public ListItem Load(BinaryReader reader)
        {
            Username = reader.ReadSimpleString();
            Reason = reader.ReadSimpleString();
            Address = reader.ReadSimpleString();
            return this;
        }
    }

    internal class ModInfo : IReadable<ModInfo>, IWritable<ModInfo>
    {
        public int Crc;
        public string Name;
        public Version Version;

        public ModInfo(BinaryReader reader)
        {
            Load(reader);
        }

        public ModInfo() { }

        public ModInfo Load(BinaryReader reader)
        {
            Name = reader.ReadSimpleString();
            Version = new Version(reader);
            Crc = reader.ReadInt32();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteSimpleString(Name);
            Version.Write(writer);
            writer.Write(Crc);
        }
    }

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

    internal class Version : IReadable<Version>, IWritable<Version>
    {
        public short MajorVersion;
        public short MinorVersion;
        public short SubVersion;

        public Version(BinaryReader reader)
        {
            Load(reader);
        }

        public Version() { }

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
    }
}