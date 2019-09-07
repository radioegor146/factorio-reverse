using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class MultiplayerConfigSettings : IReadable<MultiplayerConfigSettings>
    {
        public string Name;
        public string Description;
        public string Password;

        public bool AllowCommands;

        public bool PublicGame;
        public bool SteamGame;
        public bool LanGame;

        public ushort MaxPlayers;

        public uint AutosaveInterval;
        public uint AfkAutokickInterval;
        public uint MaxUploadInKiloBytesPerSecond;
        public uint MaxUploadSlots;

        public bool AutosaveOnlyOnServer;
        public bool NonBlockingSaving;
        public bool IgnorePlayerLimitForReturningPlayers;
        public bool OnlyAdminsCanPauseTheGame;
        public bool RequireUserVerification;
        public bool EnableWhitelist;

        public string[] Tags;

        public MultiplayerConfigSettings(BinaryReader reader)
        {
            Load(reader);
        }

        public MultiplayerConfigSettings() { }

        public MultiplayerConfigSettings Load(BinaryReader reader)
        {
            Name = reader.ReadFactorioString();
            Description = reader.ReadFactorioString();
            Password = reader.ReadFactorioString();

            AllowCommands = reader.ReadBoolean();

            PublicGame = reader.ReadBoolean();
            SteamGame = reader.ReadBoolean();
            LanGame = reader.ReadBoolean();

            MaxPlayers = reader.ReadUInt16();

            AutosaveInterval = reader.ReadUInt32();
            AfkAutokickInterval = reader.ReadUInt32();
            MaxUploadInKiloBytesPerSecond = reader.ReadUInt32();
            MaxUploadSlots = reader.ReadUInt32();

            AutosaveOnlyOnServer = reader.ReadBoolean();
            NonBlockingSaving = reader.ReadBoolean();
            IgnorePlayerLimitForReturningPlayers = reader.ReadBoolean();
            OnlyAdminsCanPauseTheGame = reader.ReadBoolean();
            RequireUserVerification = reader.ReadBoolean();
            EnableWhitelist = reader.ReadBoolean();

            if (reader.ReadBoolean())
                Tags = reader.ReadArray(x => x.ReadFactorioString());
            return this;
        }
    }
}