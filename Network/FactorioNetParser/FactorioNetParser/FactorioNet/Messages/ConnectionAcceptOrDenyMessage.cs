using System.IO;
using System.Linq;
using FactorioNetParser.FactorioNet.Data;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class ConnectionAcceptOrDenyMessage : IMessage<ConnectionAcceptOrDenyMessage>
    {
        public int ClientRequestId;

        public byte Status;
        public string GameName;
        public string ServerHash;
        public string Description;
        public byte Latency;
        public int GameId;

        public long UnknownRead0; // unknown & unused

        public string ServerUsername;
        public byte MapSavingProgress;
        public short[] SavingFor;
        public Client[] Clients;

        public uint FirstSequenceNumberToSend;
        public uint FirstSequenceNumberToExpect;
        public short NewPeerId;

        public ModInfo[] Mods;
        public ModSetting[] ModSettings;

        public short PausedBy;

        public int GameId0;
        public long UnknownRead1; // probably LanGameId, but 'v5->LANgameID = -1;', unused
        public string Name;
        public Version ApplicationVersion;
        public ushort BuildVersion;
        public string ServerDescription;
        public short MaxPlayers;
        public int GameTimeElapsed;
        public bool HasPassword;
        public byte SocketType;
        public string HostAddress;

        public bool PublicGame;
        public bool SteamGame;
        public bool LanGame;

        public string[] Tags;
        public string ServerUsername1;
        public string PasswordHash;

        public int AutosaveInterval;
        public int AutosaveSlots;
        public int AfkAutoKickInterval;
        public bool AllowCommands;

        public int MaxUploadInKilobytesPerSecond;
        public int MaxUploadSlots;
        public byte MinimumLatencyInTicks;
        public bool IgnorePlayerLimitForReturningPlayers;
        public bool OnlyAdminsCanPauseTheGame;
        public bool RequireUserVerification;
        public bool WhitelistEnabled;
        
        public ListItem[] Admins;
        public AddressToUsernameMapping[] AdminMappings;
        public ListItem[] Banlist;
        public AddressToUsernameMapping[] BanlistMappings;
        public ListItem[] Whitelist;
        public AddressToUsernameMapping[] WhitelistMappings;
        
        public bool Online;

        public ConnectionAcceptOrDenyMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public ConnectionAcceptOrDenyMessage() { }

        public MessageType GetMessageType() => MessageType.ConnectionAcceptOrDeny;

        private class VarShort : IReadable<VarShort>, IWritable<VarShort>
        {
            public short Value;

            public VarShort()
            {

            }

            public VarShort(short value)
            {
                Value = value;
            }

            public VarShort Load(BinaryReader reader)
            {
                Value = reader.ReadVarShort();
                return this;
            }

            public void Write(BinaryWriter writer)
            {
                writer.WriteVarShort(Value);
            }
        }

        public ConnectionAcceptOrDenyMessage Load(BinaryReader reader)
        {
            ClientRequestId = reader.ReadInt32();
            Status = reader.ReadByte();
            GameName = reader.ReadSimpleString();
            ServerHash = reader.ReadSimpleString();
            Description = reader.ReadSimpleString();
            Latency = reader.ReadByte();
            GameId = reader.ReadInt32();

            UnknownRead0 = reader.ReadInt64();

            ServerUsername = reader.ReadSimpleString();
            MapSavingProgress = reader.ReadByte();
            SavingFor = reader.ReadSimpleArray<VarShort>().Select(x => x.Value).ToArray();
            Clients = reader.ReadSimpleArray<Client>();

            FirstSequenceNumberToExpect = reader.ReadUInt32();
            FirstSequenceNumberToSend = reader.ReadUInt32();
            NewPeerId = reader.ReadInt16();

            Mods = reader.ReadArray<ModInfo>();
            ModSettings = reader.ReadArray<ModSetting>();

            PausedBy = reader.ReadInt16();

            GameId0 = reader.ReadInt32();
            UnknownRead1 = reader.ReadInt64();
            Name = reader.ReadSimpleString();
            ApplicationVersion = new Version(reader);
            BuildVersion = reader.ReadUInt16();
            ServerDescription = reader.ReadFactorioString();
            MaxPlayers = reader.ReadInt16();
            GameTimeElapsed = reader.ReadInt32();
            HasPassword = reader.ReadBoolean();
            SocketType = reader.ReadByte();
            HostAddress = reader.ReadFactorioString();

            PublicGame = reader.ReadBoolean();
            SteamGame = reader.ReadBoolean();
            LanGame = reader.ReadBoolean();

            Tags = reader.ReadArray(IOExtensions.ReadSimpleString);
            ServerUsername1 = reader.ReadFactorioString();
            PasswordHash = reader.ReadFactorioString();
            AutosaveInterval = reader.ReadInt32();
            AutosaveSlots = reader.ReadInt32();
            AfkAutoKickInterval = reader.ReadInt32();
            AllowCommands = reader.ReadBoolean();
            MaxUploadInKilobytesPerSecond = reader.ReadInt32();
            MaxUploadSlots = reader.ReadInt32();
            MinimumLatencyInTicks = reader.ReadByte();
            IgnorePlayerLimitForReturningPlayers = reader.ReadBoolean();
            OnlyAdminsCanPauseTheGame = reader.ReadBoolean();
            RequireUserVerification = reader.ReadBoolean();
            WhitelistEnabled = reader.ReadBoolean();

            Admins = reader.ReadArray<ListItem>();
            AdminMappings = reader.ReadArray<AddressToUsernameMapping>();
            Whitelist = reader.ReadArray<ListItem>();
            WhitelistMappings = reader.ReadArray<AddressToUsernameMapping>();
            Banlist = reader.ReadArray<ListItem>();
            BanlistMappings = reader.ReadArray<AddressToUsernameMapping>();

            Online = true;
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(ClientRequestId);
            writer.Write(Status);
            writer.WriteSimpleString(GameName);
            writer.WriteSimpleString(ServerHash);
            writer.WriteSimpleString(Description);
            writer.Write(Latency);
            writer.Write(GameId);

            writer.Write(UnknownRead0);

            writer.WriteSimpleString(ServerUsername);
            writer.Write(MapSavingProgress);
            writer.WriteSimpleArray(SavingFor.Select(x => new VarShort(x)).ToArray());
            writer.WriteSimpleArray(Clients);

            writer.Write(FirstSequenceNumberToExpect);
            writer.Write(FirstSequenceNumberToSend);
            writer.Write(NewPeerId);

            writer.WriteArray(Mods);
            writer.WriteArray(ModSettings);

            writer.Write(PausedBy);

            writer.Write(GameId0);
            writer.Write(UnknownRead1);
            writer.WriteSimpleString(Name);
            writer.Write(ApplicationVersion);
            writer.Write(BuildVersion);
            writer.WriteFactorioString(ServerDescription);
            writer.Write(MaxPlayers);
            writer.Write(GameTimeElapsed);
            writer.Write(HasPassword);
            writer.Write(SocketType);
            writer.WriteFactorioString(HostAddress);

            writer.Write(PublicGame);
            writer.Write(SteamGame);
            writer.Write(LanGame);
            writer.WriteArray(Tags, IOExtensions.WriteSimpleString);
            writer.WriteFactorioString(ServerUsername1);
            writer.WriteFactorioString(PasswordHash);
            writer.Write(AutosaveInterval);
            writer.Write(AutosaveSlots);
            writer.Write(AfkAutoKickInterval);
            writer.Write(AllowCommands);
            writer.Write(MaxUploadInKilobytesPerSecond);
            writer.Write(MaxUploadSlots);
            writer.Write(MinimumLatencyInTicks);
            writer.Write(IgnorePlayerLimitForReturningPlayers);
            writer.Write(OnlyAdminsCanPauseTheGame);
            writer.Write(RequireUserVerification);
            writer.Write(WhitelistEnabled);
            writer.WriteArray(Admins);
            writer.WriteArray(AdminMappings);
            writer.WriteArray(Whitelist);
            writer.WriteArray(WhitelistMappings);
            writer.WriteArray(Banlist);
            writer.WriteArray(BanlistMappings);
        }
    }
}