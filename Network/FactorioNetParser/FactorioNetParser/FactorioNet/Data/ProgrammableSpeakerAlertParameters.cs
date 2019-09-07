using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class ProgrammableSpeakerAlertParameters : IReadable<ProgrammableSpeakerAlertParameters>
    {
        public string AlertMessage;
        public SignalId IconSignalId;
        public bool ShowAlert;
        public bool ShowOnMap;

        public ProgrammableSpeakerAlertParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public ProgrammableSpeakerAlertParameters() { }

        public ProgrammableSpeakerAlertParameters Load(BinaryReader reader)
        {
            ShowAlert = reader.ReadBoolean();
            ShowOnMap = reader.ReadBoolean();
            IconSignalId = new SignalId(reader);
            AlertMessage = reader.ReadFactorioString();
            return this;
        }
    }
}