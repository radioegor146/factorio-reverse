using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FactorioNetParser.FactorioNet.Messages
{
    internal class PixelPosition : IReadable<PixelPosition>
    {
        public int X;
        public int Y;

        public PixelPosition(BinaryReader reader)
        {
            Load(reader);
        }

        public PixelPosition() { }

        public PixelPosition Load(BinaryReader reader)
        {
            X = reader.ReadInt32();
            Y = reader.ReadInt32();
            return this;
        }
    }

    internal class Direction : IReadable<Direction>
    {
        public byte Value;

        public Direction(BinaryReader reader)
        {
            Load(reader);
        }

        public Direction() { }

        public Direction Load(BinaryReader reader)
        {
            Value = reader.ReadByte();
            return this;
        }
    }

    internal class RidingState : IReadable<RidingState>
    {
        public byte AccelerationState;
        public byte Direction;

        public RidingState(BinaryReader reader)
        {
            Load(reader);
        }

        public RidingState() { }

        public RidingState Load(BinaryReader reader)
        {
            Direction = reader.ReadByte();
            AccelerationState = reader.ReadByte();
            return this;
        }
    }

    internal class ItemStackTargetSpecification : IReadable<ItemStackTargetSpecification>
    {
        public byte InventoryIndex;
        public ushort SlotIndex;
        public byte Source;
        public byte Target;

        public ItemStackTargetSpecification(BinaryReader reader)
        {
            Load(reader);
        }

        public ItemStackTargetSpecification() { }

        public ItemStackTargetSpecification Load(BinaryReader reader)
        {
            InventoryIndex = reader.ReadByte();
            SlotIndex = reader.ReadUInt16();
            Source = reader.ReadByte();
            Target = reader.ReadByte();
            return this;
        }
    }

    internal class SignalId : IReadable<SignalId>
    {
        public byte ContainedType;
        public ushort FluidId;
        public ushort ItemId;
        public ushort VirtualSignalId;

        public SignalId(BinaryReader reader)
        {
            Load(reader);
        }

        public SignalId() { }

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
    }

    internal class CircuitCondition : IReadable<CircuitCondition>
    {
        public byte Comparator;
        public SignalId FirstSignalId;
        public int SecondConstant;
        public bool SecondItemIsConstant;
        public SignalId SecondSignalId;

        public CircuitCondition(BinaryReader reader)
        {
            Load(reader);
        }

        public CircuitCondition() { }

        public CircuitCondition Load(BinaryReader reader)
        {
            Comparator = reader.ReadByte();
            FirstSignalId = new SignalId(reader);
            SecondSignalId = new SignalId(reader);
            SecondConstant = reader.ReadInt32();
            SecondItemIsConstant = reader.ReadBoolean();
            return this;
        }
    }

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

    internal class Vector : IReadable<Vector>
    {
        public double X;
        public double Y;

        public Vector(BinaryReader reader)
        {
            Load(reader);
        }

        public Vector() { }

        public Vector Load(BinaryReader reader)
        {
            X = reader.ReadDouble();
            Y = reader.ReadDouble();
            return this;
        }
    }

    internal class RealOrientation : IReadable<RealOrientation>
    {
        public float Orientation;

        public RealOrientation(BinaryReader reader)
        {
            Load(reader);
        }

        public RealOrientation() { }

        public RealOrientation Load(BinaryReader reader)
        {
            Orientation = reader.ReadSingle();
            return this;
        }
    }

    internal class SetupBlueprintData : IReadable<SetupBlueprintData>
    {
        public ReplacementEntityData[] EntitiesToChange;
        public uint[] ExcludedEntities;
        public uint[] ExcludedTiles;
        public SignalId[] Icons;
        public bool IncludeEntities;
        public bool IncludeModules;
        public bool IncludeStationNames;
        public bool IncludeTiles;
        public ReplacementTileData[] TilesToChange;

        public SetupBlueprintData(BinaryReader reader)
        {
            Load(reader);
        }

        public SetupBlueprintData() { }

        public SetupBlueprintData Load(BinaryReader reader)
        {
            IncludeModules = reader.ReadBoolean();
            IncludeEntities = reader.ReadBoolean();
            IncludeTiles = reader.ReadBoolean();
            IncludeStationNames = reader.ReadBoolean();

            ExcludedEntities = reader.ReadArray(x => x.ReadUInt32());
            ExcludedTiles = reader.ReadArray(x => x.ReadUInt32());
            EntitiesToChange = reader.ReadArray<ReplacementEntityData>();
            TilesToChange = reader.ReadArray<ReplacementTileData>();
            Icons = reader.ReadArray<SignalId>();
            return this;
        }
    }

    internal class BlueprintRecordId : IReadable<BlueprintRecordId>
    {
        public ushort Id;
        public ushort PlayerIndex;

        public BlueprintRecordId(BinaryReader reader)
        {
            Load(reader);
        }

        public BlueprintRecordId() { }

        public BlueprintRecordId Load(BinaryReader reader)
        {
            PlayerIndex = reader.ReadUInt16();
            Id = reader.ReadUInt16();
            return this;
        }
    }

    internal class UpdateBlueprintShelfData : IReadable<UpdateBlueprintShelfData>
    {
        public ushort NextRecordId;
        public AddBlueprintRecordData[] RecordsToAdd;
        public ushort[] RecordsToRemove;
        public UpdateBlueprintData[] RecordsToUpdate;
        public ushort ShelfPlayerIndex;
        public uint Timestamp;

        public UpdateBlueprintShelfData(BinaryReader reader)
        {
            Load(reader);
        }

        public UpdateBlueprintShelfData() { }

        public UpdateBlueprintShelfData Load(BinaryReader reader)
        {
            ShelfPlayerIndex = reader.ReadUInt16();
            NextRecordId = reader.ReadUInt16();
            Timestamp = reader.ReadUInt32();
            RecordsToRemove = reader.ReadArray(x => x.ReadUInt16());
            RecordsToAdd = reader.ReadArray<AddBlueprintRecordData>();
            RecordsToUpdate = reader.ReadArray<UpdateBlueprintData>();
            return this;
        }
    }

    internal class UpdateBlueprintData : IReadable<UpdateBlueprintData>
    {
        public ushort Id;
        public byte[] NewHash = new byte[20];
        public string NewLabel;

        public UpdateBlueprintData(BinaryReader reader)
        {
            Load(reader);
        }

        public UpdateBlueprintData() { }

        public UpdateBlueprintData Load(BinaryReader reader)
        {
            Id = reader.ReadUInt16();
            NewHash = reader.ReadBytes(20);
            NewLabel = reader.ReadFactorioString();
            return this;
        }
    }

    internal class AddBlueprintRecordData : IReadable<AddBlueprintRecordData>
    {
        public ushort AddInBook;
        public SignalId[] BlueprintIcons;
        public byte[] Hash = new byte[20];
        public ushort Id;
        public bool IsBook;
        public ushort ItemId;
        public string Label;
        public SingleRecordDataInBook[] PreviewsInBook;

        public AddBlueprintRecordData(BinaryReader reader)
        {
            Load(reader);
        }

        public AddBlueprintRecordData() { }

        public AddBlueprintRecordData Load(BinaryReader reader)
        {
            Id = reader.ReadUInt16();
            Hash = reader.ReadBytes(20);
            ItemId = reader.ReadUInt16();
            IsBook = reader.ReadBoolean();
            BlueprintIcons = reader.ReadArray<SignalId>();
            Label = reader.ReadFactorioString();
            AddInBook = reader.ReadUInt16();
            if (IsBook)
                PreviewsInBook = reader.ReadArray<SingleRecordDataInBook>();
            return this;
        }
    }

    internal class SingleRecordDataInBook : IReadable<SingleRecordDataInBook>
    {
        public SignalId[] BlueprintIcons;
        public byte[] Hash = new byte[20];
        public ushort Id;
        public ushort ItemId;
        public string Label;

        public SingleRecordDataInBook(BinaryReader reader)
        {
            Load(reader);
        }

        public SingleRecordDataInBook() { }

        public SingleRecordDataInBook Load(BinaryReader reader)
        {
            Id = reader.ReadUInt16();
            ItemId = reader.ReadUInt16();

            Hash = reader.ReadBytes(20);
            BlueprintIcons = reader.ReadArray<SignalId>();
            Label = reader.ReadFactorioString();
            return this;
        }
    }

    internal class PlayerJoinGameData : IReadable<PlayerJoinGameData>
    {
        public byte ForceId;
        public ushort PeerId;
        public string Username;

        public PlayerJoinGameData() { }

        public PlayerJoinGameData(BinaryReader reader)
        {
            Load(reader);
        }

        public PlayerJoinGameData Load(BinaryReader reader)
        {
            PeerId = (ushort) reader.ReadVarShort();
            ForceId = reader.ReadByte();
            Username = reader.ReadFactorioString();
            return this;
        }
    }

    internal class ReplacementTileData : IReadable<ReplacementTileData>
    {
        public Class1[] var0;

        public ReplacementTileData(BinaryReader reader)
        {
            Load(reader);
        }

        public ReplacementTileData() { }

        public ReplacementTileData Load(BinaryReader reader)
        {
            var0 = reader.ReadArray<Class1>();
            return this;
        }
    }

    internal class ReplacementEntityData : IReadable<ReplacementEntityData>
    {
        public Class0[] var0;

        public ReplacementEntityData(BinaryReader reader)
        {
            Load(reader);
        }

        public ReplacementEntityData() { }

        public ReplacementEntityData Load(BinaryReader reader)
        {
            var0 = reader.ReadArray<Class0>();
            return this;
        }
    }

    internal class ArithmeticCombinatorParameters : IReadable<ArithmeticCombinatorParameters>
    {
        public int FirstConstant;
        public SignalId FirstSignalId;
        public bool FirstSignalIsConstant;
        public byte Operation;
        public SignalId OutputSignalId;
        public int SecondConstant;
        public SignalId SecondSignalId;
        public bool SecondSignalIsConstant;

        public ArithmeticCombinatorParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public ArithmeticCombinatorParameters() { }

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
    }

    internal class DeciderCombinatorParameters : IReadable<DeciderCombinatorParameters>
    {
        public byte Comparator;
        public bool CopyCountFromInput;
        public SignalId FirstSignalId;
        public bool FirstSignalIsConstant;
        public SignalId OutputSignalId;
        public int SecondConstant;
        public SignalId SecondSignalId;

        public DeciderCombinatorParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public DeciderCombinatorParameters() { }

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
    }

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

    internal class BuildTerrainParameters : IReadable<BuildTerrainParameters>
    {
        public bool CreatedByMoving;
        public bool ShiftBuild;
        public byte Size;
        public bool SkipFogOfWar;

        public BuildTerrainParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public BuildTerrainParameters() { }

        public BuildTerrainParameters Load(BinaryReader reader)
        {
            CreatedByMoving = reader.ReadBoolean();
            Size = reader.ReadByte();
            ShiftBuild = reader.ReadBoolean();
            SkipFogOfWar = reader.ReadBoolean();
            return this;
        }
    }

    internal class TrainWaitCondition : IReadable<TrainWaitCondition>
    {
        public byte Action;
        public byte AddType;
        public uint ConditionIndex;
        public uint ScheldueIndex;

        public TrainWaitCondition(BinaryReader reader)
        {
            Load(reader);
        }

        public TrainWaitCondition() { }

        public TrainWaitCondition Load(BinaryReader reader)
        {
            Action = reader.ReadByte();
            AddType = reader.ReadByte();
            ScheldueIndex = reader.ReadUInt32();
            ConditionIndex = reader.ReadUInt32();
            return this;
        }
    }

    internal class TrainWaitConditionData : IReadable<TrainWaitConditionData>
    {
        public byte ConditionCompareType;
        public uint ConditionIndex;
        public uint ConditionTicks;
        public byte ConditionType;
        public uint ScheldueIndex;

        public TrainWaitConditionData(BinaryReader reader)
        {
            Load(reader);
        }

        public TrainWaitConditionData() { }

        public TrainWaitConditionData Load(BinaryReader reader)
        {
            ScheldueIndex = reader.ReadUInt32();
            ConditionIndex = reader.ReadUInt32();
            ConditionType = reader.ReadByte();
            ConditionCompareType = reader.ReadByte();
            ConditionTicks = reader.ReadUInt32();
            return this;
        }
    }

    internal class ExtendedBitBuffer : IReadable<ExtendedBitBuffer>
    {
        public uint Bits;
        public uint[] Data;

        public ExtendedBitBuffer(BinaryReader reader)
        {
            Load(reader);
        }

        public ExtendedBitBuffer() { }

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
    }

    internal class BuildRailData : IReadable<BuildRailData>
    {
        public bool AlternativeBuild;
        public ExtendedBitBuffer Buffer;
        public Direction Direction;
        public byte Mode;
        public int StartX;
        public int StartY;

        public BuildRailData(BinaryReader reader)
        {
            Load(reader);
        }

        public BuildRailData() { }

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
    }

    internal class InfinityContainerFilterItemData : IReadable<InfinityContainerFilterItemData>
    {
        public uint Count;
        public ushort FilterIndex;
        public ushort ItemId;
        public byte Mode;

        public InfinityContainerFilterItemData(BinaryReader reader)
        {
            Load(reader);
        }

        public InfinityContainerFilterItemData() { }

        public InfinityContainerFilterItemData Load(BinaryReader reader)
        {
            ItemId = reader.ReadUInt16();
            Mode = reader.ReadByte();
            FilterIndex = reader.ReadUInt16();
            Count = reader.ReadUInt32();
            return this;
        }
    }

    internal class EditPermissionGroupParameters : IReadable<EditPermissionGroupParameters>
    {
        public ushort ActionIndex;
        public int GroupId;
        public string NewGroupName;
        public ushort PlayerIndex;
        public byte Type;

        public EditPermissionGroupParameters(BinaryReader reader)
        {
            Load(reader);
        }

        public EditPermissionGroupParameters() { }

        public EditPermissionGroupParameters Load(BinaryReader reader)
        {
            GroupId = reader.ReadVarInt();
            PlayerIndex = reader.ReadUInt16();
            ActionIndex = reader.ReadUInt16();
            NewGroupName = reader.ReadFactorioString();
            Type = reader.ReadByte();
            return this;
        }
    }

    internal class ChooseElemId : IReadable<ChooseElemId>
    {
        public ushort EntityId;
        public ushort FluidId;
        public ushort ItemId;
        public ushort RecipeId;
        public SignalId SignalId;
        public ushort TileId;

        public ChooseElemId(BinaryReader reader)
        {
            Load(reader);
        }

        public ChooseElemId() { }

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
    }

    public enum InputActionType : byte
    {
        Nothing,
        StopWalking,
        BeginMining,
        StopMining,
        ToggleDriving,
        OpenGui,
        CloseGui,
        OpenCharacterGui,
        ConnectRollingStock,
        DisconnectRollingStock,
        SelectedEntityCleared,
        CleanCursorStack,
        ResetAssemblingMachine,
        OpenTechnologyGui,
        LaunchRocket,
        ChangeActiveQuickBar,
        OpenBlueprintLibraryGui,
        OpenProductionGui,
        OpenKillsGui,
        StopRepair,
        CancelNewBlueprint,
        CloseBlueprintRecord,
        CopyEntitySettings,
        PasteEntitySettings,
        DestroyOpenedItem,
        ToggleShowEntityInfo,
        SingleplayerInit,
        MultiplayerInit,
        SwitchToRenameStopGui,
        OpenBonusGui,
        OpenTrainsGui,
        OpenAchievementsGui,
        OpenTutorialsGui,
        CycleBlueprintBookForwards,
        CycleBlueprintBookBackwards,
        StopMovementInTheNextTick,
        ToggleEnableVehicleLogisticsWhileMoving,
        ToggleDeconstructionItemEntityFilterMode,
        ToggleDeconstructionItemTileFilterMode,
        OpenLogisticGui,
        CancelDropBlueprintRecord,
        DropItem,
        BuildItem,
        StartWalking,
        BeginMiningTerrain,
        ChangeRidingState,
        OpenItem,
        OpenModItem,
        OpenEquipment,
        CursorTransfer,
        CursorSplit,
        StackTransfer,
        InventoryTransfer,
        CheckCrcHeuristic,
        Craft,
        WireDragging,
        ChangeShootingState,
        SetupAssemblingMachine,
        SelectedEntityChanged,
        SmartPipette,
        StackSplit,
        InventorySplit,
        CancelCraft,
        SetFilter,
        CheckCrc,
        SetCircuitCondition,
        SetSignal,
        StartResearch,
        SetLogisticFilterItem,
        SetLogisticFilterSignal,
        SetCircuitModeOfOperation,
        GuiClick,
        WriteToConsole,
        MarketOffer,
        EditTrainSchedule,
        ChangeTrainStopStation,
        ChangeActiveItemGroupForCrafting,
        GuiTextChanged,
        GuiCheckedStateChanged,
        GuiSelectionStateChanged,
        GuiValueChanged,
        PlaceEquipment,
        TakeEquipment,
        UseAbility,
        UseItem,
        UseArtilleryRemote,
        SetInventoryBar,
        ChangeActiveItemGroupForFilters,
        MoveOnZoom,
        StartRepair,
        Deconstruct,
        SelectBlueprintEntities,
        AltSelectBlueprintEntities,
        SetupBlueprint,
        SetupSingleBlueprintRecord,
        SetSingleBlueprintRecordIcon,
        OpenBlueprintRecord,
        CloseBlueprintBook,
        ChangeSingleBlueprintRecordLabel,
        GrabBlueprintRecord,
        DropBlueprintRecord,
        DeleteBlueprintRecord,
        CreateBlueprintLike,
        CreateBlueprintLikeStackTransfer,
        UpdateBlueprintShelf,
        TransferBlueprint,
        TransferBlueprintImmediately,
        ChangeBlueprintBookRecordLabel,
        RemoveCables,
        ExportBlueprint,
        ImportBlueprint,
        PlayerJoinGame,
        CancelDeconstruct,
        ChangeArithmeticCombinatorParameters,
        ChangeDeciderCombinatorParameters,
        ChangeProgrammableSpeakerParameters,
        ChangeProgrammableSpeakerAlertParameters,
        ChangeProgrammableSpeakerCircuitParameters,
        BuildTerrain,
        ChangeTrainWaitCondition,
        ChangeTrainWaitConditionData,
        CustomInput,
        ChangeItemLabel,
        BuildRail,
        CancelResearch,
        OpenTrainStationGui,
        SelectArea,
        AltSelectArea,
        ServerCommand,
        ClearSelectedBlueprint,
        ClearSelectedDeconstructionItem,
        SetLogisticTrashFilterItem,
        SetInfinityContainerFilterItem,
        ModSettingsChanged,
        SetEntityEnergyProperty,
        EditCustomTag,
        EditPermissionGroup,
        ImportBlueprintString,
        GuiElemChanged,
        BlueprintTransferQueueUpdate,
        DragTrainSchedule,
        DragTrainWaitCondition,
        SelectItem,
        SelectEntitySlot,
        SelectTileSlot,
        DisplayResolutionChanged,
        DisplayScaleChanged,
        SetSplitterPriority,
        ChangePickingState,
        SelectedEntityChangedVeryClose,
        SelectedEntityChangedVeryClosePrecise,
        SelectedEntityChangedRelative,
        SelectedEntityChangedBasedOnUnitNumber,
        ShortcutQuickBarTransfer,
        SelectGun,
        SetAutosortInventory,
        SetAutoLaunchRocket,
        SwitchConstantCombinatorState,
        SwitchPowerSwitchState,
        SwitchConnectToLogisticNetwork,
        SetBehaviorMode,
        FastEntityTransfer,
        RotateEntity,
        FastEntitySplit,
        SetTrainStopped,
        ChangeControllerSpeed,
        SetAllowCommands,
        SetResearchFinishedStopsGame,
        SetInserterMaxStackSize,
        OpenTrainGui,
        SetEntityColor,
        SetDeconstructionItemTreesAndRocksOnly,
        SetDeconstructionItemTileSelectionMode,
        DropToBlueprintBook,
        DeleteCustomTag,
        DeletePermissionGroup,
        AddPermissionGroup,
        SetInfinityContainerRemoveUnfilteredItems,
        SetCarWeaponsControl,
        SetRequestFromBuffers,
        PlayerLeaveGame
    }

    internal class InputAction : IReadable<InputAction>
    {
        public InputActionType Action;
        public List<object> Data = new List<object>();
        public short PlayerIndex;

        public InputAction(BinaryReader reader)
        {
            Load(reader);
        }

        public InputAction() { }

        public InputAction Load(BinaryReader reader)
        {
            Action = (InputActionType)reader.ReadByte();
            PlayerIndex = reader.ReadVarShort();
            switch (Action)
            {
                case InputActionType.Nothing:
                case InputActionType.StopWalking:
                case InputActionType.BeginMining:
                case InputActionType.StopMining:
                case InputActionType.ToggleDriving:
                case InputActionType.OpenGui:
                case InputActionType.CloseGui:
                case InputActionType.OpenCharacterGui:
                case InputActionType.ConnectRollingStock:
                case InputActionType.DisconnectRollingStock:
                case InputActionType.SelectedEntityCleared:
                case InputActionType.CleanCursorStack:
                case InputActionType.ResetAssemblingMachine:
                case InputActionType.OpenTechnologyGui:
                case InputActionType.LaunchRocket:
                case InputActionType.ChangeActiveQuickBar:
                case InputActionType.OpenBlueprintLibraryGui:
                case InputActionType.OpenProductionGui:
                case InputActionType.OpenKillsGui:
                case InputActionType.StopRepair:
                case InputActionType.CancelNewBlueprint:
                case InputActionType.CloseBlueprintRecord:
                case InputActionType.CopyEntitySettings:
                case InputActionType.PasteEntitySettings:
                case InputActionType.DestroyOpenedItem:
                case InputActionType.ToggleShowEntityInfo:
                case InputActionType.SingleplayerInit:
                case InputActionType.MultiplayerInit:
                case InputActionType.SwitchToRenameStopGui:
                case InputActionType.OpenBonusGui:
                case InputActionType.OpenTrainsGui:
                case InputActionType.OpenAchievementsGui:
                case InputActionType.OpenTutorialsGui:
                case InputActionType.CycleBlueprintBookForwards:
                case InputActionType.CycleBlueprintBookBackwards:
                case InputActionType.StopMovementInTheNextTick:
                case InputActionType.ToggleEnableVehicleLogisticsWhileMoving:
                case InputActionType.ToggleDeconstructionItemEntityFilterMode:
                case InputActionType.ToggleDeconstructionItemTileFilterMode:
                case InputActionType.OpenLogisticGui:
                case InputActionType.CancelDropBlueprintRecord:
                    return this;
                case InputActionType.DropItem:
                case InputActionType.BeginMiningTerrain:
                case InputActionType.WireDragging:
                case InputActionType.SelectedEntityChanged:
                case InputActionType.UseItem:
                case InputActionType.UseArtilleryRemote:
                case InputActionType.StartRepair:
                case InputActionType.RemoveCables:
                    Add(new PixelPosition(reader));
                    break;
                case InputActionType.BuildItem:
                    Add(new PixelPosition(reader));
                    Add(new Direction(reader));
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.StartWalking:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.ChangeRidingState:
                    Add(new RidingState(reader));
                    break;
                case InputActionType.OpenItem:
                case InputActionType.OpenModItem:
                case InputActionType.CursorTransfer:
                case InputActionType.CursorSplit:
                case InputActionType.StackTransfer:
                case InputActionType.InventoryTransfer:
                case InputActionType.StackSplit:
                case InputActionType.InventorySplit:
                case InputActionType.SetInventoryBar:
                case InputActionType.ClearSelectedBlueprint:
                case InputActionType.ClearSelectedDeconstructionItem:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.OpenEquipment:
                    Add(new PixelPosition(reader));
                    Add(reader.ReadByte());
                    break;
                case InputActionType.CheckCrcHeuristic:
                case InputActionType.CheckCrc:
                case InputActionType.MarketOffer:
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    break;
                case InputActionType.ChangeShootingState:
                    Add(reader.ReadByte());
                    Add(new PixelPosition(reader));
                    break;
                case InputActionType.SetupAssemblingMachine:
                case InputActionType.SmartPipette:
                case InputActionType.StartResearch:
                case InputActionType.UseAbility:
                case InputActionType.CreateBlueprintLike:
                case InputActionType.CreateBlueprintLikeStackTransfer:
                case InputActionType.CustomInput:
                case InputActionType.CancelResearch:
                case InputActionType.CancelCraft:
                case InputActionType.SelectItem:
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.SelectEntitySlot:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.SetFilter:
                    Add(new ItemStackTargetSpecification(reader));
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.SetCircuitCondition:
                    Add(reader.ReadByte());
                    Add(new CircuitCondition(reader));
                    break;
                case InputActionType.SetSignal:
                    Add(new SignalId(reader));
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.SetLogisticFilterItem:
                case InputActionType.SetLogisticTrashFilterItem:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SetLogisticFilterSignal:
                    Add(new SignalId(reader));
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.SetCircuitModeOfOperation:
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.GuiClick:
                case InputActionType.GuiCheckedStateChanged:
                    Add(new GuiChangedData(reader));
                    break;
                case InputActionType.WriteToConsole:
                case InputActionType.ChangeTrainStopStation:
                case InputActionType.ChangeSingleBlueprintRecordLabel:
                case InputActionType.ChangeItemLabel:
                case InputActionType.OpenTrainStationGui:
                case InputActionType.ImportBlueprintString:
                    Add(reader.ReadFactorioString());
                    break;
                case InputActionType.EditTrainSchedule:
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    Add(reader.ReadFactorioString());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.ChangeActiveItemGroupForCrafting:
                case InputActionType.ChangeActiveItemGroupForFilters:
                case InputActionType.PlayerLeaveGame:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.GuiTextChanged:
                    Add(new GuiChangedData(reader));
                    Add(reader.ReadFactorioString());
                    break;
                case InputActionType.GuiSelectionStateChanged:
                    Add(new GuiChangedData(reader));
                    Add(reader.ReadInt32());
                    break;
                case InputActionType.GuiValueChanged:
                    Add(new GuiChangedData(reader));
                    Add(reader.ReadInt64());
                    break;
                case InputActionType.PlaceEquipment:
                case InputActionType.TakeEquipment:
                    Add(new PixelPosition(reader));
                    Add(reader.ReadByte());
                    break;
                case InputActionType.MoveOnZoom:
                    Add(new Vector(reader));
                    break;
                case InputActionType.Deconstruct:
                case InputActionType.SelectBlueprintEntities:
                case InputActionType.AltSelectBlueprintEntities:
                case InputActionType.CancelDeconstruct:
                case InputActionType.SelectArea:
                case InputActionType.AltSelectArea:
                    Add(new PixelPosition(reader));
                    Add(new PixelPosition(reader));
                    Add(new RealOrientation(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SetupBlueprint:
                case InputActionType.SetupSingleBlueprintRecord:
                    Add(new SetupBlueprintData(reader));
                    break;
                case InputActionType.SetSingleBlueprintRecordIcon:
                    Add(new SignalId(reader));
                    Add(reader.ReadByte());
                    break;
                case InputActionType.OpenBlueprintRecord:
                case InputActionType.CloseBlueprintBook:
                case InputActionType.GrabBlueprintRecord:
                case InputActionType.DeleteBlueprintRecord:
                case InputActionType.ImportBlueprint:
                    Add(new BlueprintRecordId(reader));
                    break;
                case InputActionType.DropBlueprintRecord:
                case InputActionType.ExportBlueprint:
                    Add(reader.ReadUInt16());
                    Add(new BlueprintRecordId(reader));
                    break;
                case InputActionType.UpdateBlueprintShelf:
                    Add(new UpdateBlueprintShelfData(reader));
                    break;
                case InputActionType.TransferBlueprint:
                case InputActionType.TransferBlueprintImmediately:
                case InputActionType.ChangeBlueprintBookRecordLabel:
                    Add(new BlueprintRecordId(reader));
                    Add(reader.ReadFactorioString());
                    break;
                case InputActionType.PlayerJoinGame:
                    Add(new PlayerJoinGameData(reader));
                    break;
                case InputActionType.ChangeArithmeticCombinatorParameters:
                    Add(new ArithmeticCombinatorParameters(reader));
                    break;
                case InputActionType.ChangeDeciderCombinatorParameters:
                    Add(new DeciderCombinatorParameters(reader));
                    break;
                case InputActionType.ChangeProgrammableSpeakerParameters:
                    Add(reader.ReadInt64());
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.ChangeProgrammableSpeakerAlertParameters:
                    Add(new ProgrammableSpeakerAlertParameters(reader));
                    break;
                case InputActionType.ChangeProgrammableSpeakerCircuitParameters:
                    Add(reader.ReadByte());
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    break;
                case InputActionType.BuildTerrain:
                    Add(new BuildTerrainParameters(reader));
                    break;
                case InputActionType.ChangeTrainWaitCondition:
                    Add(new TrainWaitCondition(reader));
                    break;
                case InputActionType.ChangeTrainWaitConditionData:
                    Add(new TrainWaitConditionData(reader));
                    break;
                case InputActionType.BuildRail:
                    Add(new BuildRailData(reader));
                    break;
                case InputActionType.ServerCommand:
                    Add(reader.ReadFactorioString());
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt64());
                    break;
                case InputActionType.SetInfinityContainerFilterItem:
                    Add(new InfinityContainerFilterItemData());
                    break;
                case InputActionType.ModSettingsChanged:
                    Add(reader.ReadUInt32());
                    // TODO fuck mod settings
                    throw new NotImplementedException("fuck mod settings");
                case InputActionType.SetEntityEnergyProperty:
                    Add(reader.ReadByte());
                    Add(reader.ReadInt64());
                    break;
                case InputActionType.EditCustomTag:
                    Add(reader.ReadInt32());
                    Add(reader.ReadFactorioString());
                    Add(new SignalId(reader));
                    Add(new PixelPosition(reader));
                    break;
                case InputActionType.EditPermissionGroup:
                    Add(new EditPermissionGroupParameters(reader));
                    break;
                case InputActionType.GuiElemChanged:
                    Add(new GuiChangedData(reader));
                    Add(new ChooseElemId(reader));
                    break;
                case InputActionType.BlueprintTransferQueueUpdate:
                    //TODO WTF with this packet
                    throw new NotImplementedException("WTF with this packet");
                case InputActionType.DragTrainSchedule:
                case InputActionType.DisplayResolutionChanged:
                    Add(new PixelPosition(reader));
                    break;
                case InputActionType.DragTrainWaitCondition:
                    Add(new PixelPosition(reader));
                    Add(reader.ReadInt32());
                    break;
                case InputActionType.SelectTileSlot:
                    Add(reader.ReadByte());
                    Add(reader.ReadInt16());
                    break;
                case InputActionType.DisplayScaleChanged:
                case InputActionType.ChangeControllerSpeed:
                    Add(reader.ReadInt64());
                    break;
                case InputActionType.SetSplitterPriority:
                case InputActionType.ChangePickingState:
                case InputActionType.SelectedEntityChangedVeryClose:
                case InputActionType.SetAutosortInventory:
                case InputActionType.SetAutoLaunchRocket:
                case InputActionType.SwitchConstantCombinatorState:
                case InputActionType.SwitchPowerSwitchState:
                case InputActionType.SwitchConnectToLogisticNetwork:
                case InputActionType.SetBehaviorMode:
                case InputActionType.FastEntityTransfer:
                case InputActionType.RotateEntity:
                case InputActionType.FastEntitySplit:
                case InputActionType.SetTrainStopped:
                case InputActionType.SetAllowCommands:
                case InputActionType.SetResearchFinishedStopsGame:
                case InputActionType.SetInserterMaxStackSize:
                case InputActionType.SetDeconstructionItemTreesAndRocksOnly:
                case InputActionType.SetDeconstructionItemTileSelectionMode:
                case InputActionType.SetInfinityContainerRemoveUnfilteredItems:
                case InputActionType.SetCarWeaponsControl:
                case InputActionType.SetRequestFromBuffers:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SelectedEntityChangedVeryClosePrecise:
                case InputActionType.ShortcutQuickBarTransfer:
                case InputActionType.SelectGun:
                case InputActionType.DropToBlueprintBook:
                    Add(reader.ReadInt16());
                    break;
                case InputActionType.SelectedEntityChangedRelative:
                case InputActionType.SelectedEntityChangedBasedOnUnitNumber:
                case InputActionType.OpenTrainGui:
                case InputActionType.SetEntityColor:
                case InputActionType.DeleteCustomTag:
                case InputActionType.DeletePermissionGroup:
                case InputActionType.AddPermissionGroup:
                    Add(reader.ReadInt32());
                    break;
                case InputActionType.Craft:
                    //TODO there is no handler for this packet o_O
                    throw new NotImplementedException("there is no handler for this packet o_O");
                default:
                    throw new Exception($"No such InputAction: 0x{Action:x2}");
            }

            return this;
        }

        private void Add(object obj)
        {
            Data.Add(obj);
        }
    }

    internal class InputActionFragment : IReadable<InputActionFragment>
    {
        public byte[] FragmentData;
        public int FragmentNumber;
        public int Id;
        public short PlayerIndex = -1;
        public int TotalFragments = 1;
        public byte Type;

        public InputActionFragment(BinaryReader reader)
        {
            Load(reader);
        }

        public InputActionFragment() { }

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
    }

    internal class TickClosure
    {
        public InputActionFragment[] InputActionFragments;
        public InputAction[] InputActions;
        public int Tick;

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

    internal class ScriptRegistrations : IReadable<ScriptRegistrations>
    {
        public uint[] var0;
        public uint[] var1;
        public byte var2;
        public byte var3;
        public byte var4;

        public ScriptRegistrations() { }

        public ScriptRegistrations(BinaryReader reader)
        {
            Load(reader);
        }

        public ScriptRegistrations Load(BinaryReader reader)
        {
            var0 = reader.ReadArray(x => x.ReadUInt32());
            var1 = reader.ReadArray(x => x.ReadUInt32());
            var2 = reader.ReadByte();
            var3 = reader.ReadByte();
            var4 = reader.ReadByte();
            return this;
        }
    }
    
    public enum SynchronizerActionType : byte
    {
        GameEnd,
        PeerDisconnect,
        NewPeerInfo,
        ClientChangedState,
        ClientShouldStartSendingTickClosures,
        MapReadyForDownload,
        MapLoadingProgressUpdate,
        MapSavingProgressUpdate,
        SavingForUpdate,
        MapDownloadingProgressUpdate,
        CatchingUpProgressUpdate,
        PeerDroppingProgressUpdate,
        PlayerDesynced,
        BeginPause,
        EndPause,
        SkippedTickClosure,
        SkippedTickClosureConfirm,
        ChangeLatency,
        IncreasedLatencyConfirm,
        SavingCountdown,
        InputActionFragmentsInFlight,
        InputActionFragmentsInFlightFinished
    }

    internal class SynchronizerAction : IReadable<SynchronizerAction>
    {

        public SynchronizerActionType Action;

        public List<object> Data = new List<object>();

        public SynchronizerAction() { }

        public SynchronizerAction(BinaryReader reader)
        {
            Load(reader);
        }

        public SynchronizerAction Load(BinaryReader reader)
        {
            Action = (SynchronizerActionType) reader.ReadByte();
            switch (Action)
            {
                case SynchronizerActionType.GameEnd:
                case SynchronizerActionType.SavingForUpdate:
                case SynchronizerActionType.PlayerDesynced:
                case SynchronizerActionType.BeginPause:
                case SynchronizerActionType.EndPause:
                case SynchronizerActionType.InputActionFragmentsInFlightFinished:
                    break;
                case SynchronizerActionType.PeerDisconnect:
                case SynchronizerActionType.ClientChangedState:
                case SynchronizerActionType.MapLoadingProgressUpdate:
                case SynchronizerActionType.MapSavingProgressUpdate:
                case SynchronizerActionType.MapDownloadingProgressUpdate:
                case SynchronizerActionType.ChangeLatency:
                case SynchronizerActionType.CatchingUpProgressUpdate:
                case SynchronizerActionType.PeerDroppingProgressUpdate:
                    Add(reader.ReadByte());
                    break;
                case SynchronizerActionType.NewPeerInfo:
                    Add(reader.ReadFactorioString());
                    break;
                case SynchronizerActionType.ClientShouldStartSendingTickClosures:
                case SynchronizerActionType.SkippedTickClosure:
                case SynchronizerActionType.SkippedTickClosureConfirm:
                case SynchronizerActionType.SavingCountdown:
                    Add(reader.ReadInt32());
                    break;
                case SynchronizerActionType.MapReadyForDownload:
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    Add(reader.ReadArray(x => Tuple.Create(reader.ReadFactorioString(), reader.ReadUInt32()))
                        .ToDictionary(x => x.Item1, y => y.Item2));
                    Add(reader.ReadArray(x => Tuple.Create(reader.ReadFactorioString(), new ScriptRegistrations(reader)))
                        .ToDictionary(x => x.Item1, y => y.Item2));
                    Add(reader.ReadArray(x =>
                            Tuple.Create(reader.ReadFactorioString(), reader.ReadArray(y => y.ReadFactorioString())))
                        .ToDictionary(x => x.Item1, y => y.Item2));
                    break;
                case SynchronizerActionType.IncreasedLatencyConfirm:
                    Add(reader.ReadInt32());
                    Add(reader.ReadByte());
                    break;
                case SynchronizerActionType.InputActionFragmentsInFlight:
                    Add(reader.ReadInt32());
                    Add(reader.ReadInt32());
                    break;
                default:
                    throw new Exception($"No such SynchronizerAction: 0x{Action:x2}");
            }

            return this;
        }

        public void Add(object a)
        {
            Data.Add(a);
        }
    }

    internal class ServerToClientHeartbeatMessage : IReadable<ServerToClientHeartbeatMessage>, IPacket
    {
        public byte DeserializationMask;
        public uint[] RequestsForHeartbeat;
        public int SequenceNumber;
        public Tuple<SynchronizerAction, ushort>[] SynchronizerActions;
        public TickClosure[] TickClosures;

        public ServerToClientHeartbeatMessage(BinaryReader reader)
        {
            Load(reader);
        }

        public ServerToClientHeartbeatMessage() { }

        public PacketType GetMessageType()
        {
            return PacketType.ServerToClientHeartbeat;
        }

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
                    TickClosures = reader.ReadArray(x => new TickClosure(x, loadTickOnly));
                }
            }

            if ((DeserializationMask & 0x10) > 0)
                SynchronizerActions = reader.ReadArray(x => Tuple.Create(new SynchronizerAction(x), x.ReadUInt16()));

            if ((DeserializationMask & 0x01) > 0) RequestsForHeartbeat = reader.ReadArray(x => x.ReadUInt32());
            return this;
        }
    }
}