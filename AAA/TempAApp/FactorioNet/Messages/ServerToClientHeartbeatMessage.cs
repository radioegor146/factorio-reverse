using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempAApp.FactorioNet.Messages
{
    class PixelPosition : IReadable<PixelPosition>
    {
        public int X;
        public int Y;
        public PixelPosition Load(BinaryReader reader)
        {
            X = reader.ReadInt32();
            Y = reader.ReadInt32();
            return this;
        }
        public PixelPosition(BinaryReader reader)
        {
            Load(reader);
        }

        public PixelPosition()
        {
        }
    }

    class Direction : IReadable<Direction>
    {
        public byte Value;
        public Direction Load(BinaryReader reader)
        {
            Value = reader.ReadByte();
            return this;
        }
        public Direction(BinaryReader reader)
        {
            Load(reader);
        }

        public Direction()
        {
        }
    }

    class RidingState : IReadable<RidingState>
    {
        public byte Direction;
        public byte AccelerationState;

        public RidingState Load(BinaryReader reader)
        {
            Direction = reader.ReadByte();
            AccelerationState = reader.ReadByte();
            return this;
        }
        public RidingState(BinaryReader reader)
        {
            Load(reader);
        }

        public RidingState()
        {
        }
    }

    class ItemStackTargetSpecification : IReadable<ItemStackTargetSpecification>
    {
        public byte InventoryIndex;
        public ushort SlotIndex;
        public byte Source;
        public byte Target;

        public ItemStackTargetSpecification Load(BinaryReader reader)
        {
            InventoryIndex = reader.ReadByte();
            SlotIndex = reader.ReadUInt16();
            Source = reader.ReadByte();
            Target = reader.ReadByte();
            return this;
        }
        public ItemStackTargetSpecification(BinaryReader reader)
        {
            Load(reader);
        }

        public ItemStackTargetSpecification()
        {
        }
    }

    class SignalId : IReadable<SignalId>
    {
        public byte ContainedType;
        public ushort FluidId;
        public ushort VirtualSignalId;
        public ushort ItemId;

        public SignalId Load(BinaryReader reader)
        {
            ContainedType = reader.ReadByte();
            switch (ContainedType)
            {
                case 0:
                    ItemId = reader.ReadUInt16();
                    break;
                case 1:
                    FluidId = reader.ReadUInt16();
                    break;
                case 2:
                    VirtualSignalId = reader.ReadUInt16();
                    break;
                default:
                    throw new Exception($"Wrong SignalId: {ContainedType}");
            }
            return this;
        }
        public SignalId(BinaryReader reader)
        {
            Load(reader);
        }

        public SignalId()
        {
        }
    }

    class CircuitCondition : IReadable<CircuitCondition>
    {
        public byte Comparator;
        public SignalId FirstSignalId;
        public SignalId SecondSignalId;
        public int SecondConstant;
        public bool SecondItemIsConstant;
        public CircuitCondition Load(BinaryReader reader)
        {
            Comparator = reader.ReadByte();
            FirstSignalId = new SignalId(reader);
            SecondSignalId = new SignalId(reader);
            SecondConstant = reader.ReadInt32();
            SecondItemIsConstant = reader.ReadBoolean();
            return this;
        }
        public CircuitCondition(BinaryReader reader)
        {
            Load(reader);
        }

        public CircuitCondition()
        {
        }
    }

    class GuiChangedData : IReadable<GuiChangedData>
    {
        public uint GuiElementIndex;
        public ushort Button;
        public bool IsAlt;
        public bool IsControl;
        public bool IsShift;

        public GuiChangedData Load(BinaryReader reader)
        {
            GuiElementIndex = reader.ReadUInt32();
            Button = reader.ReadUInt16();
            IsAlt = reader.ReadBoolean();
            IsControl = reader.ReadBoolean();
            IsShift = reader.ReadBoolean();
            return this;
        }
        public GuiChangedData(BinaryReader reader)
        {
            Load(reader);
        }

        public GuiChangedData()
        {
        }
    }

    class Vector : IReadable<Vector>
    {
        public double X;
        public double Y;

        public Vector Load(BinaryReader reader)
        {
            X = reader.ReadDouble();
            Y = reader.ReadDouble();
            return this;
        }
        public Vector(BinaryReader reader)
        {
            Load(reader);
        }

        public Vector()
        {
        }
    }

    class RealOrientation : IReadable<RealOrientation>
    {
        public float Orientation;

        public RealOrientation Load(BinaryReader reader)
        {
            Orientation = reader.ReadSingle();
            return this;
        }
        public RealOrientation(BinaryReader reader)
        {
            Load(reader);
        }

        public RealOrientation()
        {
        }
    }

    class SetupBlueprintData : IReadable<SetupBlueprintData>
    {
        public bool IncludeModules;
        public bool IncludeEntities;
        public bool IncludeTiles;
        public bool IncludeStationNames;
        public uint[] ExcludedEntities;
        public uint[] ExcludedTiles;
        public ReplacementEntityData[] EntitiesToChange;
        public ReplacementTileData[] TilesToChange;
        public SignalId[] Icons;

        public SetupBlueprintData Load(BinaryReader reader)
        {
            IncludeModules = reader.ReadBoolean();
            IncludeEntities = reader.ReadBoolean();
            IncludeTiles = reader.ReadBoolean();
            IncludeStationNames = reader.ReadBoolean();

            ExcludedEntities = reader.ReadArray((x) => x.ReadUInt32());
            ExcludedTiles = reader.ReadArray((x) => x.ReadUInt32());
            EntitiesToChange = reader.ReadArray<ReplacementEntityData>();
            TilesToChange = reader.ReadArray<ReplacementTileData>();
            Icons = reader.ReadArray<SignalId>();
            return this;
        }
        public SetupBlueprintData(BinaryReader reader)
        {
            Load(reader);
        }

        public SetupBlueprintData()
        {
        }
    }

    class BlueprintRecordId : IReadable<BlueprintRecordId>
    {
        public ushort PlayerIndex;
        public ushort Id;

        public BlueprintRecordId Load(BinaryReader reader)
        {
            PlayerIndex = reader.ReadUInt16();
            Id = reader.ReadUInt16();
            return this;
        }
        public BlueprintRecordId(BinaryReader reader)
        {
            Load(reader);
        }

        public BlueprintRecordId()
        {
        }
    }

    class UpdateBlueprintShelfData : IReadable<UpdateBlueprintShelfData>
    {
        public ushort ShelfPlayerIndex;
        public ushort NextRecordId;
        public uint Timestamp;
        public ushort[] RecordsToRemove;
        public AddBlueprintRecordData[] RecordsToAdd;
        public UpdateBlueprintData[] RecordsToUpdate;

        public UpdateBlueprintShelfData Load(BinaryReader reader)
        {
            ShelfPlayerIndex = reader.ReadUInt16();
            NextRecordId = reader.ReadUInt16();
            Timestamp = reader.ReadUInt32();
            RecordsToRemove = reader.ReadArray((x) => x.ReadUInt16());
            RecordsToAdd = reader.ReadArray<AddBlueprintRecordData>();
            RecordsToUpdate = reader.ReadArray<UpdateBlueprintData>();
            return this;
        }
        public UpdateBlueprintShelfData(BinaryReader reader)
        {
            Load(reader);
        }

        public UpdateBlueprintShelfData()
        {
        }
    }

    class UpdateBlueprintData : IReadable<UpdateBlueprintData>
    {
        public ushort Id;
        public byte[] NewHash = new byte[20];
        public string NewLabel;

        public UpdateBlueprintData Load(BinaryReader reader)
        {
            Id = reader.ReadUInt16();
            NewHash = reader.ReadBytes(20);
            NewLabel = reader.ReadComplexString();
            return this;
        }
        public UpdateBlueprintData(BinaryReader reader)
        {
            Load(reader);
        }

        public UpdateBlueprintData()
        {
        }
    }

    class AddBlueprintRecordData : IReadable<AddBlueprintRecordData>
    {
        public ushort Id;
        public byte[] Hash = new byte[20];
        public ushort ItemId;
        public bool IsBook;
        public SignalId[] BlueprintIcons;
        public string Label;
        public ushort AddInBook;
        public SingleRecordDataInBook[] PreviewsInBook; //if isbook

        public AddBlueprintRecordData Load(BinaryReader reader)
        {
            Id = reader.ReadUInt16();
            Hash = reader.ReadBytes(20);
            ItemId = reader.ReadUInt16();
            IsBook = reader.ReadBoolean();
            BlueprintIcons = reader.ReadArray<SignalId>();
            Label = reader.ReadComplexString();
            AddInBook = reader.ReadUInt16();
            if (IsBook)
            {
                PreviewsInBook = reader.ReadArray<SingleRecordDataInBook>();
            }
            return this;
        }
        public AddBlueprintRecordData(BinaryReader reader)
        {
            Load(reader);
        }

        public AddBlueprintRecordData()
        {
        }
    }

    class SingleRecordDataInBook : IReadable<SingleRecordDataInBook>
    {
        public ushort Id;
        public ushort ItemId;
        public byte[] Hash = new byte[20];
        public SignalId[] BlueprintIcons;
        public string Label;

        public SingleRecordDataInBook Load(BinaryReader reader)
        {
            Id = reader.ReadUInt16();
            ItemId = reader.ReadUInt16();

            Hash = reader.ReadBytes(20);
            BlueprintIcons = reader.ReadArray<SignalId>();
            Label = reader.ReadComplexString();
            return this;
        }
        public SingleRecordDataInBook(BinaryReader reader)
        {
            Load(reader);
        }

        public SingleRecordDataInBook()
        {
        }
    }

    class ReplacementTileData : IReadable<ReplacementTileData>
    {
        public Class1[] var0;

        public ReplacementTileData Load(BinaryReader reader)
        {
            var0 = reader.ReadArray<Class1>();
            return this;
        }
        public ReplacementTileData(BinaryReader reader)
        {
            Load(reader);
        }

        public ReplacementTileData()
        {
        }
    }

    class ReplacementEntityData : IReadable<ReplacementEntityData>
    {
        public Class0[] var0;

        public ReplacementEntityData Load(BinaryReader reader)
        {
            var0 = reader.ReadArray<Class0>();
            return this;
        }
        public ReplacementEntityData(BinaryReader reader)
        {
            Load(reader);
        }

        public ReplacementEntityData()
        {
        }
    }

    class InputAction
    {
        public byte Action;
        public short PlayerIndex;
        public List<object> Data = new List<object>();

        private void Add(object obj)
        {
            Data.Add(obj);
        }

        public InputAction(BinaryReader reader)
        {
            Action = reader.ReadByte();
            PlayerIndex = reader.ReadVarShort();
            switch (Action)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 0xA:
                case 0xB:
                case 0xC:
                case 0xD:
                case 0xE:
                case 0xF:
                case 0x10:
                case 0x11:
                case 0x12:
                case 0x13:
                case 0x14:
                case 0x15:
                case 0x16:
                case 0x17:
                case 0x18:
                case 0x19:
                case 0x1A:
                case 0x1B:
                case 0x1C:
                case 0x1D:
                case 0x1E:
                case 0x1F:
                case 0x20:
                case 0x21:
                case 0x22:
                case 0x23:
                case 0x24:
                case 0x25:
                case 0x26:
                case 0x27:
                case 0x28:
                    return;
                case 0x29:
                case 0x2C:
                case 0x37:
                case 0x3A:
                case 0x54:
                case 0x55:
                case 0x59:
                case 0x6C:
                    Add(new PixelPosition(reader));
                    break;
                case 0x2A:
                    Add(new PixelPosition(reader));
                    Add(new Direction(reader));
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    break;
                case 0x2B:
                    Add(reader.ReadByte());
                    break;
                case 0x2D:
                    Add(new RidingState(reader));
                    break;
                case 0x2E:
                case 0x2F:
                case 0x31:
                case 0x32:
                case 0x33:
                case 0x34:
                case 0x3C:
                case 0x3D:
                case 0x56:
                case 0x81:
                case 0x82:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case 0x30:
                    Add(new PixelPosition(reader));
                    Add(reader.ReadByte());
                    break;
                case 0x35:
                case 0x40:
                case 0x49:
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    break;
                case 0x38:
                    Add(reader.ReadByte());
                    Add(new PixelPosition(reader));
                    break;
                case 0x39:
                case 0x3B:
                case 0x43:
                case 0x53:
                case 0x66:
                case 0x67:
                case 0x79:
                case 0x7C:
                case 0x3E:
                case 0x8E:
                    Add(reader.ReadUInt16());
                    break;
                case 0x8F:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt16());
                    break;
                case 0x3F:
                    Add(new ItemStackTargetSpecification(reader));
                    Add(reader.ReadUInt16());
                    break;
                case 0x41:
                    Add(reader.ReadByte());
                    Add(new CircuitCondition(reader));
                    break;
                case 0x42:
                    Add(new SignalId(reader));
                    Add(reader.ReadUInt16());
                    break;
                case 0x44:
                case 0x83:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt32());
                    break;
                case 0x45:
                    Add(new SignalId(reader));
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt16());
                    break;
                case 0x46:
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    break;
                case 0x47:
                case 0x4e:
                    Add(new GuiChangedData(reader));
                    break;
                case 0x48:
                case 0x4b:
                case 0x62:
                case 0x7a:
                case 0x7d:
                case 0x89:
                    Add(reader.ReadComplexString());
                    break;
                case 0x4a:
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    Add(reader.ReadComplexString());
                    Add(reader.ReadUInt32());
                    break;
                case 0x4c:
                case 0x57:
                case 0xb4:
                    Add(reader.ReadByte());
                    break;
                case 0x4d:
                    Add(new GuiChangedData(reader));
                    Add(reader.ReadComplexString());
                    break;
                case 0x4f:
                    Add(new GuiChangedData(reader));
                    Add(reader.ReadInt32());
                    break;
                case 0x50:
                    Add(new GuiChangedData(reader));
                    Add(reader.ReadInt64());
                    break;
                case 0x51:
                case 0x52:
                    Add(new PixelPosition(reader));
                    Add(reader.ReadByte());
                    break;
                case 0x58:
                    Add(new Vector(reader));
                    break;
                case 0x5a:
                case 0x5b:
                case 0x5c:
                case 0x70:
                case 0x7e:
                case 0x7f:
                    Add(new PixelPosition(reader));
                    Add(new PixelPosition(reader));
                    Add(new RealOrientation(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    break;
                case 0x5d:
                case 0x5e:
                    Add(new SetupBlueprintData(reader));
                    break;
                case 0x5f:
                    Add(new SignalId(reader));
                    Add(reader.ReadByte());
                    break;
                case 0x60:
                case 0x61:
                case 0x63:
                case 0x65:
                case 0x6e:
                    Add(new BlueprintRecordId(reader));
                    break;
                case 0x64:
                case 0x6d:
                    Add(reader.ReadUInt16());
                    Add(new BlueprintRecordId(reader));
                    break;
                case 0x68:
                    Add(new UpdateBlueprintShelfData(reader));
                    break;
                default:
                    throw new Exception($"No such InputAction: 0x{Action:x2}");
            }
        }
    }

    class InputActionFragment : IReadable<InputActionFragment>
    {
        public byte Type;
        public int Id;
        public short PlayerIndex = -1;
        public int TotalFragments = 1;
        public int FragmentNumber;
        public byte[] FragmentData;

        public InputActionFragment Load(BinaryReader reader)
        {
            Type = reader.ReadByte();
            if (Type == 0x48 || Type == 0x89 || Type == 0x68 || Type == 0x69)
            {
                Id = reader.ReadInt32();
                PlayerIndex = reader.ReadVarShort();
                TotalFragments = reader.ReadVarInt();
                FragmentNumber = reader.ReadVarInt();
            }
            FragmentData = reader.ReadBytes(reader.ReadVarInt());
            return this;
        }
        public InputActionFragment(BinaryReader reader)
        {
            Load(reader);
        }

        public InputActionFragment()
        {
        }
    }

    class TickClosure
    {
        public int Tick;
        public InputAction[] InputActions;
        public InputActionFragment[] InputActionFragments;
        
        public TickClosure(BinaryReader reader, bool loadTickOnly)
        {
            Tick = reader.ReadInt32();
            if (loadTickOnly)
                return;
            var tCount = reader.ReadVarInt();
            var inputActionsCount = tCount >> 1;
            var shouldReadFragments = (tCount & 0x01) > 0;
            InputActions = new InputAction[stream.ReadVarInt()];
            for (int i = 0; i < InputActions.Length; i++)
                InputActions[i] = new InputAction(reader);
            if (!shouldReadFragments)
                return;
            InputActionFragments = reader.ReadArray<InputActionFragment>();
        }
    }

    class SynchronizerAction
    {
        public SynchronizerAction(BinaryReader reader)
        {

        }
    }

    class ServerToClientHeartbeatMessage : IReadable<ServerToClientHeartbeatMessage>
    {
        public byte DeserializationMask;
        public int SequenceNumber;
        public TickClosure[] TickClosures;
        public SynchronizerAction[] SynchronizerActions;
        public uint[] RequestsForHeartbeat;

        public ServerToClientHeartbeatMessage Load(BinaryReader reader)
        {
            DeserializationMask = reader.ReadByte();
            SequenceNumber = reader.ReadInt32();
            if ((DeserializationMask & 0x02) > 0)
            {
                var loadTickOnly = (DeserializationMask & 0x08) > 0;
                if ((DeserializationMask & 0x04) > 0)
                {
                    TickClosures = new TickClosure[1];
                    TickClosures[0] = new TickClosure(reader, loadTickOnly);
                }
                else
                {
                    TickClosures = new TickClosure[reader.ReadVarInt()];
                    for (var i = 0; i < TickClosures.Length; i++)
                        TickClosures[i] = new TickClosure(reader, loadTickOnly);
                }
            }

            if ((DeserializationMask & 0x10) > 0)
            {
                SynchronizerActions = new SynchronizerAction[reader.ReadVarInt()];
                for (var i = 0; i < SynchronizerActions.Length; i++)
                    SynchronizerActions[i] = new SynchronizerAction(reader);
            }

            if ((DeserializationMask & 0x01) > 0)
            {
                RequestsForHeartbeat = new uint[reader.ReadVarInt()];
                for (var i = 0; i < RequestsForHeartbeat.Length; i++)
                    RequestsForHeartbeat[i] = reader.ReadUInt32();
            }
            return this;
        }
        public ServerToClientHeartbeatMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public ServerToClientHeartbeatMessage()
        {
        }
    }
}
