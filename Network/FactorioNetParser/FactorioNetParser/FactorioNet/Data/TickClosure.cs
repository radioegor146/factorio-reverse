using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class TickClosure : IReadable<TickClosure>, IWritable<TickClosure>
    {
        public InputActionSegment[] InputActionSegments;
        public InputAction[] InputActions;

        public TickClosure(BinaryReader reader)
        {
            Load(reader);
        }

        public TickClosure Load(BinaryReader reader)
        {
            var tCount = reader.ReadVarInt();
            var inputActionsCount = tCount >> 1;
            var shouldReadSegments = (tCount & 0x01) > 0;
            InputActions = new InputAction[inputActionsCount];
            for (var i = 0; i < InputActions.Length; i++)
                InputActions[i] = new InputAction(reader);

            if (!shouldReadSegments)
                return this;
            InputActionSegments = reader.ReadArray<InputActionSegment>();
            return this;
        }

        public void Write(BinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}