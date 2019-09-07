using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class GuiChangedData : IReadable<GuiChangedData>
    {
        public ushort Button;
        public uint GuiElementIndex;
        public bool IsAlt;
        public bool IsControl;
        public bool IsShift;

        public GuiChangedData(BinaryReader reader)
        {
            Load(reader);
        }

        public GuiChangedData() { }

        public GuiChangedData Load(BinaryReader reader)
        {
            GuiElementIndex = reader.ReadUInt32();
            Button = reader.ReadUInt16();
            IsAlt = reader.ReadBoolean();
            IsControl = reader.ReadBoolean();
            IsShift = reader.ReadBoolean();
            return this;
        }
    }
}