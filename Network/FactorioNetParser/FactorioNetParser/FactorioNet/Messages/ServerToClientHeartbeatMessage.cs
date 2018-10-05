using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FactorioNetParser.FactorioNet.Messages
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

    class PlayerJoinGameData : IReadable<PlayerJoinGameData>
    {
        public ushort PeerId;
        public byte ForceId;
        public string Username;

        public PlayerJoinGameData Load(BinaryReader reader)
        {
            PeerId = (ushort)reader.ReadVarShort();
            ForceId = reader.ReadByte();
            Username = reader.ReadComplexString();
            return this;
        }

        public PlayerJoinGameData()
        {
        }

        public PlayerJoinGameData(BinaryReader reader)
        {
            Load(reader);
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

    class ArithmeticCombinatorParameters : IReadable<ArithmeticCombinatorParameters>
    {
        public SignalId FirstSignalId;
        public SignalId SecondSignalId;
        public SignalId OutputSignalId;
        public int SecondConstant;
        public byte Operation;
        public bool SecondSignalIsConstant;
        public int FirstConstant;
        public bool FirstSignalIsConstant;

        public ArithmeticCombinatorParameters Load(BinaryReader reader)
        {
            FirstSignalId = new SignalId(reader);
            SecondSignalId = new SignalId(reader);
            OutputSignalId = new SignalId(reader);
            SecondConstant = reader.ReadInt32();
            Operation = reader.ReadByte();
            SecondSignalIsConstant = reader.ReadBoolean();
            FirstConstant = reader.ReadInt32();
            FirstSignalIsConstant = reader.ReadBoolean();
            return this;
        }

        public ArithmeticCombinatorParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public ArithmeticCombinatorParameters()
        {

        }
    }

    class DeciderCombinatorParameters : IReadable<DeciderCombinatorParameters>
    {
        public SignalId FirstSignalId;
        public SignalId SecondSignalId;
        public SignalId OutputSignalId;
        public int SecondConstant;
        public byte Comparator;
        public bool CopyCountFromInput;
        public bool FirstSignalIsConstant;

        public DeciderCombinatorParameters Load(BinaryReader reader)
        {
            FirstSignalId = new SignalId(reader);
            SecondSignalId = new SignalId(reader);
            OutputSignalId = new SignalId(reader);
            SecondConstant = reader.ReadInt32();
            Comparator = reader.ReadByte();
            CopyCountFromInput = reader.ReadBoolean();
            FirstSignalIsConstant = reader.ReadBoolean();
            return this;
        }

        public DeciderCombinatorParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public DeciderCombinatorParameters()
        {
        }
    }

    class ProgrammableSpeakerAlertParameters : IReadable<ProgrammableSpeakerAlertParameters>
    {
        public bool ShowAlert;
        public bool ShowOnMap;
        public SignalId IconSignalId;
        public string AlertMessage;

        public ProgrammableSpeakerAlertParameters Load(BinaryReader reader)
        {
            ShowAlert = reader.ReadBoolean();
            ShowOnMap = reader.ReadBoolean();
            IconSignalId = new SignalId(reader);
            AlertMessage = reader.ReadComplexString();
            return this;
        }

        public ProgrammableSpeakerAlertParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public ProgrammableSpeakerAlertParameters()
        {
        }
    }

    class BuildTerrainParameters : IReadable<BuildTerrainParameters>
    {
        public bool CreatedByMoving;
        public byte Size;
        public bool ShiftBuild;
        public bool SkipFogOfWar;

        public BuildTerrainParameters Load(BinaryReader reader)
        {
            CreatedByMoving = reader.ReadBoolean();
            Size = reader.ReadByte();
            ShiftBuild = reader.ReadBoolean();
            SkipFogOfWar = reader.ReadBoolean();
            return this;
        }

        public BuildTerrainParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public BuildTerrainParameters()
        {
        }
    }

    class TrainWaitCondition : IReadable<TrainWaitCondition>
    {
        public byte Action;
        public byte AddType;
        public uint ScheldueIndex;
        public uint ConditionIndex;

        public TrainWaitCondition Load(BinaryReader reader)
        {
            Action = reader.ReadByte();
            AddType = reader.ReadByte();
            ScheldueIndex = reader.ReadUInt32();
            ConditionIndex = reader.ReadUInt32();
            return this;
        }

        public TrainWaitCondition(BinaryReader reader)
        {
            Load(reader);
        }

        public TrainWaitCondition()
        {
        }
    }

    class TrainWaitConditionData : IReadable<TrainWaitConditionData>
    {
        public uint ScheldueIndex;
        public uint ConditionIndex;
        public byte ConditionType;
        public byte ConditionCompareType;
        public uint ConditionTicks;

        public TrainWaitConditionData Load(BinaryReader reader)
        {
            ScheldueIndex = reader.ReadUInt32();
            ConditionIndex = reader.ReadUInt32();
            ConditionType = reader.ReadByte();
            ConditionCompareType = reader.ReadByte();
            ConditionTicks = reader.ReadUInt32();
            return this;
        }

        public TrainWaitConditionData(BinaryReader reader)
        {
            Load(reader);
        }

        public TrainWaitConditionData()
        {
        }
    }

    class ExtendedBitBuffer : IReadable<ExtendedBitBuffer>
    {
        public uint[] Data;
        public uint Bits;

        public ExtendedBitBuffer Load(BinaryReader reader)
        {
            var tValue = reader.ReadVarInt();
            Data = new uint[tValue >> 5];
            for (var i = 0; i < Data.Length; i++)
                Data[i] = reader.ReadUInt32();
            if ((tValue & 0x1F) > 0)
                Bits = reader.ReadUInt32();
            return this;
        }

        public ExtendedBitBuffer(BinaryReader reader)
        {
            Load(reader);
        }

        public ExtendedBitBuffer()
        {
        }
    }

    class BuildRailData : IReadable<BuildRailData>
    {
        public byte Mode;
        public int StartX;
        public int StartY;
        public Direction Direction;
        public ExtendedBitBuffer Buffer;
        public bool AlternativeBuild;

        public BuildRailData Load(BinaryReader reader)
        {
            Mode = reader.ReadByte();
            StartX = reader.ReadInt32();
            StartY = reader.ReadInt32();
            Direction = new Direction(reader);
            Buffer = new ExtendedBitBuffer(reader);
            AlternativeBuild = reader.ReadBoolean();
            return this;
        }

        public BuildRailData(BinaryReader reader)
        {
            Load(reader);
        }

        public BuildRailData()
        {
        }
    }

    class InfinityContainerFilterItemData : IReadable<InfinityContainerFilterItemData>
    {
        public ushort ItemId;
        public byte Mode;
        public ushort FilterIndex;
        public uint Count;

        public InfinityContainerFilterItemData Load(BinaryReader reader)
        {
            ItemId = reader.ReadUInt16();
            Mode = reader.ReadByte();
            FilterIndex = reader.ReadUInt16();
            Count = reader.ReadUInt32();
            return this;
        }

        public InfinityContainerFilterItemData(BinaryReader reader)
        {
            Load(reader);
        }

        public InfinityContainerFilterItemData()
        {
        }
    }

    class EditPermissionGroupParameters : IReadable<EditPermissionGroupParameters>
    {
        public int GroupId;
        public ushort PlayerIndex;
        public ushort ActionIndex;
        public string NewGroupName;
        public byte Type;

        public EditPermissionGroupParameters Load(BinaryReader reader)
        {
            GroupId = reader.ReadVarInt();
            PlayerIndex = reader.ReadUInt16();
            ActionIndex = reader.ReadUInt16();
            NewGroupName = reader.ReadComplexString();
            Type = reader.ReadByte();
            return this;
        }

        public EditPermissionGroupParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public EditPermissionGroupParameters()
        { 
        }
    }

    class ChooseElemId : IReadable<ChooseElemId>
    {
        public ushort ItemId;
        public ushort EntityId;
        public ushort TileId;
        public ushort FluidId;
        public ushort RecipeId;
        public SignalId SignalId;

        public ChooseElemId Load(BinaryReader reader)
        {
            ItemId = reader.ReadUInt16();
            EntityId = reader.ReadUInt16();
            TileId = reader.ReadUInt16();
            FluidId = reader.ReadUInt16();
            RecipeId = reader.ReadUInt16();
            SignalId = new SignalId(reader);
            return this;
        }

        public ChooseElemId(BinaryReader reader)
        {
            Load(reader);
        }

        public ChooseElemId()
        {
        }
    }

    class InputAction : IReadable<InputAction>
    {
        public byte Action;
        public short PlayerIndex;
        public List<object> Data = new List<object>();

        private void Add(object obj)
        {
            Data.Add(obj);
        }

        public InputAction Load(BinaryReader reader)
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
                    return this;
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
                case 0x4E:
                    Add(new GuiChangedData(reader));
                    break;
                case 0x48:
                case 0x4B:
                case 0x62:
                case 0x7A:
                case 0x7D:
                case 0x89:
                    Add(reader.ReadComplexString());
                    break;
                case 0x4A:
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    Add(reader.ReadComplexString());
                    Add(reader.ReadUInt32());
                    break;
                case 0x4C:
                case 0x57:
                case 0xB4:
                    Add(reader.ReadByte());
                    break;
                case 0x4D:
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
                case 0x5A:
                case 0x5B:
                case 0x5C:
                case 0x70:
                case 0x7E:
                case 0x7F:
                    Add(new PixelPosition(reader));
                    Add(new PixelPosition(reader));
                    Add(new RealOrientation(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    break;
                case 0x5d:
                case 0x5E:
                    Add(new SetupBlueprintData(reader));
                    break;
                case 0x5F:
                    Add(new SignalId(reader));
                    Add(reader.ReadByte());
                    break;
                case 0x60:
                case 0x61:
                case 0x63:
                case 0x65:
                case 0x6E:
                    Add(new BlueprintRecordId(reader));
                    break;
                case 0x64:
                case 0x6D:
                    Add(reader.ReadUInt16());
                    Add(new BlueprintRecordId(reader));
                    break;
                case 0x68:
                    Add(new UpdateBlueprintShelfData(reader));
                    break;
                case 0x69:
                case 0x6A:
                case 0x6B:
                    Add(new BlueprintRecordId(reader));
                    Add(reader.ReadComplexString());
                    break;
                case 0x6F:
                    Add(new PlayerJoinGameData(reader));
                    break;
                case 0x71:
                    Add(new ArithmeticCombinatorParameters(reader));
                    break;
                case 0x72:
                    Add(new DeciderCombinatorParameters(reader));
                    break;
                case 0x73:
                    Add(reader.ReadInt64());
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    break;
                case 0x74:
                    Add(new ProgrammableSpeakerAlertParameters(reader));
                    break;
                case 0x75:
                    Add(reader.ReadByte());
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    break;
                case 0x76:
                    Add(new BuildTerrainParameters(reader));
                    break;
                case 0x77:
                    Add(new TrainWaitCondition(reader));
                    break;
                case 0x78:
                    Add(new TrainWaitConditionData(reader));
                    break;
                case 0x7B:
                    Add(new BuildRailData(reader));
                    break;
                case 0x80:
                    Add(reader.ReadComplexString());
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt64());
                    break;
                case 0x84:
                    Add(new InfinityContainerFilterItemData());
                    break;
                case 0x85:
                    Add(reader.ReadUInt32());
                    // TODO fuck mod settings
                    throw new NotImplementedException();
                case 0x86:
                    Add(reader.ReadByte());
                    Add(reader.ReadInt64());
                    break;
                case 0x87:
                    Add(reader.ReadInt32());
                    Add(reader.ReadComplexString());
                    Add(new SignalId(reader));
                    Add(new PixelPosition(reader));
                    break;
                case 0x88:
                    Add(new EditPermissionGroupParameters(reader));
                    break;
                case 0x8A:
                    Add(new GuiChangedData(reader));
                    Add(new ChooseElemId(reader));
                    break;
                case 0x8B:
                    //TODO WTF with this packet
                    throw new NotImplementedException();
                case 0x8C:
                case 0x91:
                    Add(new PixelPosition(reader));
                    break;
                case 0x8D:
                    Add(new PixelPosition(reader));
                    Add(reader.ReadInt32());
                    break;
                case 0x90:
                    Add(reader.ReadByte());
                    Add(reader.ReadInt16());
                    break;
                case 0x92:
                case 0xA5:
                    Add(reader.ReadInt64());
                    break;
                case 0x93:
                case 0x94:
                case 0x95:
                case 0x9B:
                case 0x9C:
                case 0x9D:
                case 0x9E:
                case 0x9F:
                case 0xA0:
                case 0xA1:
                case 0xA2:
                case 0xA3:
                case 0xA4:
                case 0xA6:
                case 0xA7:
                case 0xA8:
                case 0xAB:
                case 0xAC:
                case 0xB1:
                case 0xB2:
                case 0xB3:
                    Add(reader.ReadByte());
                    break;
                case 0x96:
                case 0x99:
                case 0x9A:
                case 0xAD:
                    Add(reader.ReadInt16());
                    break;
                case 0x97:
                case 0x98:
                case 0xA9:
                case 0xAA:
                case 0xAE:
                case 0xAF:
                case 0xB0:
                    Add(reader.ReadInt32());
                    break;
                default:
                    throw new Exception($"No such InputAction: 0x{Action:x2}");
            }
            return this;
        }
        public InputAction(BinaryReader reader)
        {
            Load(reader);
        }
        public InputAction()
        {

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
            InputActions = new InputAction[inputActionsCount];
            for (var i = 0; i < InputActions.Length; i++)
                InputActions[i] = new InputAction(reader);
            if (!shouldReadFragments)
                return;
            InputActionFragments = reader.ReadArray<InputActionFragment>();
        }
    }

    class ScriptRegistrations : IReadable<ScriptRegistrations>
    {
        public uint[] var0;
        public uint[] var1;
        public byte var2;
        public byte var3;
        public byte var4;
        public ScriptRegistrations()
        {
        }

        public ScriptRegistrations Load(BinaryReader reader)
        {
            var0 = reader.ReadArray((x) => x.ReadUInt32());
            var1 = reader.ReadArray((x) => x.ReadUInt32());
            var2 = reader.ReadByte();
            var3 = reader.ReadByte();
            var4 = reader.ReadByte();
            return this;
        }
        public ScriptRegistrations(BinaryReader reader)
        {
            Load(reader);
        }
    }

    class SynchronizerAction : IReadable<SynchronizerAction>
    {
        public enum ActionType : byte
        {
            SimpleSynchronizer_0,
            PeerDisconnect,
            NewPeerInfo,
            ClientChangedState,
            ClientShouldStartSendingTickClosures,
            MapReadyForDownloadAction,
            ProgressUpdate_6,
            ProgressUpdate_7,
            SimpleSynchronizer_8,
            ProgressUpdate_9,
            ProgressUpdate_A,
            ProgressUpdate_B,
            SimpleSynchronizer_C,
            SimpleSynchronizer_D,
            SimpleSynchronizer_E,
            SkippedTickClosure,
            SkippedTickClosureConfirm,
            ChangeLatency,
            IncreasedLatencyConfirm,
            SavingCountdown,
            InputActionFragmentsInFlight,
            SimpleSynchronizer_15
        }

        public ActionType Action;

        public SynchronizerAction()
        {
        }

        public SynchronizerAction(BinaryReader reader)
        {
            Load(reader);
        }

        public List<object> Data = new List<object>();
        public void Add(object a)
        {
            Data.Add(a);
        }

        public SynchronizerAction Load(BinaryReader reader)
        {
            Action = (ActionType)reader.ReadByte();
            switch ((byte)Action)
            {
                case 0x00:
                case 0x08:
                case 0x0C:
                case 0x0D:
                case 0x0E:
                case 0x15:
                    break;
                case 0x01:
                case 0x03:
                case 0x06:
                case 0x07:
                case 0x09:
                case 0x11:
                case 0x0A:
                case 0x0B:
                    Add(reader.ReadByte());
                    break;
                case 0x02:
                    Add(reader.ReadComplexString());
                    break;
                case 0x04:
                case 0x0F:
                case 0x10:
                case 0x13:
                    Add(reader.ReadInt32());
                    break;
                case 0x05:
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    Add(reader.ReadArray((x) => Tuple.Create(reader.ReadComplexString(), reader.ReadUInt32()))
                        .ToDictionary((x) => x.Item1, (y) => y.Item2));
                    Add(reader.ReadArray((x) => Tuple.Create(reader.ReadComplexString(), new ScriptRegistrations(reader)))
                        .ToDictionary((x) => x.Item1, (y) => y.Item2));
                    Add(reader.ReadArray((x) => Tuple.Create(reader.ReadComplexString(), reader.ReadArray((y) => y.ReadComplexString())))
                        .ToDictionary((x) => x.Item1, (y) => y.Item2));
                    break;
                case 0x12:
                    Add(reader.ReadInt32());
                    Add(reader.ReadByte());
                    break;
                case 0x14:
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    break;
                default:
                    throw new Exception($"No such SynchronizerAction: 0x{Action:x2}");
            }
            return this;
        }
    }

    class ServerToClientHeartbeatMessage : IReadable<ServerToClientHeartbeatMessage>
    {
        public byte DeserializationMask;
        public int SequenceNumber;
        public TickClosure[] TickClosures;
        public Tuple<SynchronizerAction, ushort>[] SynchronizerActions;
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
                    TickClosures = reader.ReadArray((x) => new TickClosure(x, loadTickOnly));
                }
            }
            
            if ((DeserializationMask & 0x10) > 0)
            {
                SynchronizerActions = reader.ReadArray((x) => Tuple.Create(new SynchronizerAction(x), x.ReadUInt16()));
            }

            if ((DeserializationMask & 0x01) > 0)
            {
                RequestsForHeartbeat = reader.ReadArray((x) => x.ReadUInt32());
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
