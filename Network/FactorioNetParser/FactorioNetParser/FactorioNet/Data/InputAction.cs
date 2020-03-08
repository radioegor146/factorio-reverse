using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class InputAction : IReadable<InputAction>, IWritable<InputAction>
    {
        [JsonConverter(typeof(StringEnumConverter))]
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
                case InputActionType.OpenBlueprintLibraryGui:
                case InputActionType.OpenProductionGui:
                case InputActionType.OpenKillsGui:
                case InputActionType.StopRepair:
                case InputActionType.CancelNewBlueprint:
                case InputActionType.CloseBlueprintRecord:
                case InputActionType.CopyEntitySettings:
                case InputActionType.PasteEntitySettings:
                case InputActionType.DestroyOpenedItem:
                case InputActionType.UpgradeOpenedBlueprint:
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
                case InputActionType.CycleClipboardForwards:
                case InputActionType.CycleClipboardBackwards:
                case InputActionType.StopMovementInTheNextTick:
                case InputActionType.ToggleEnableVehicleLogisticsWhileMoving:
                case InputActionType.ToggleDeconstructionItemEntityFilterMode:
                case InputActionType.ToggleDeconstructionItemTileFilterMode:
                case InputActionType.OpenLogisticGui:
                case InputActionType.CancelDropBlueprintRecord:
                case InputActionType.SelectNextValidGun:
                case InputActionType.ToggleMapEditor:
                case InputActionType.DeleteBlueprintLibrary:
                case InputActionType.GameCreatedFromScenario:
                case InputActionType.ActivateCopy:
                case InputActionType.ActivateCut:
                case InputActionType.ActivatePaste:
                case InputActionType.Undo:
                case InputActionType.TogglePersonalRoboport:
                case InputActionType.ToggleEquipmentMovementBonus:
                case InputActionType.StopBuildingByMoving:
                    break;
                case InputActionType.DropItem:
                    Add(new PixelPosition(reader));
                    break;
                case InputActionType.BuildItem:
                    Add(new PixelPosition(reader));
                    Add(new Direction(reader));
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.StartWalking:
                    Add(new Direction(reader));
                    break;
                case InputActionType.BeginMiningTerrain:
                    Add(new PixelPosition(reader));
                    break;
                case InputActionType.ChangeRidingState:
                    Add(new RidingState(reader));
                    break;
                case InputActionType.OpenItem:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.OpenModItem:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.OpenEquipment:
                    Add(new TilePosition(reader));
                    Add(reader.ReadByte());
                    break;
                case InputActionType.CursorTransfer:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.CursorSplit:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.StackTransfer:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.InventoryTransfer:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.CheckCRCHeuristic:
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.Craft:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.WireDragging:
                    Add(new PixelPosition(reader));
                    break;
                case InputActionType.ChangeShootingState:
                    Add(reader.ReadByte());
                    Add(new PixelPosition(reader));
                    break;
                case InputActionType.SetupAssemblingMachine:
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.SelectedEntityChanged:
                    Add(new PixelPosition(reader));
                    break;
                case InputActionType.SmartPipette:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.StackSplit:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.InventorySplit:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.CancelCraft:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.SetFilter:
                    Add(new ItemStackTargetSpecification(reader));
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.CheckCRC:
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SetCircuitCondition:
                    Add(reader.ReadByte());
                    Add(new CircuitCondition(reader));
                    break;
                case InputActionType.SetSignal:
                    Add(new SignalId(reader));
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.StartResearch:
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.SetLogisticFilterItem:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SetLogisticFilterSignal:
                    Add(new SignalId(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SetCircuitModeOfOperation:
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.GuiClick:
                    Add(new GuiChangedData(reader));
                    break;
                case InputActionType.GuiConfirmed:
                    Add(new GuiChangedData(reader));
                    break;
                case InputActionType.WriteToConsole:
                    Add(reader.ReadFactorioString());
                    break;
                case InputActionType.MarketOffer:
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.AddTrainStation:
                    var name = reader.ReadFactorioString();
                    Add(name);
                    if (name.Length == 0)
                    {
                        Add(new PixelPosition(reader));
                    }
                    Add(reader.ReadByte());
                    break;
                case InputActionType.ChangeTrainStopStation:
                    Add(reader.ReadFactorioString());
                    break;
                case InputActionType.ChangeActiveItemGroupForCrafting:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.GuiTextChanged:
                    Add(new GuiChangedData(reader));
                    Add(reader.ReadFactorioString());
                    break;
                case InputActionType.GuiCheckedStateChanged:
                    Add(new GuiChangedData(reader));
                    break;
                case InputActionType.GuiSelectionStateChanged:
                    Add(new GuiChangedData(reader));
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.GuiSelectedTabChanged:
                    Add(new GuiChangedData(reader));
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.GuiValueChanged:
                    Add(new GuiChangedData(reader));
                    Add(reader.ReadUInt64());
                    break;
                case InputActionType.GuiSwitchStateChanged:
                    Add(new GuiChangedData(reader));
                    Add(reader.ReadByte());
                    break;
                case InputActionType.GuiLocationChanged:
                    Add(new GuiChangedData(reader));
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.PlaceEquipment:
                    Add(new TilePosition(reader));
                    Add(reader.ReadByte());
                    break;
                case InputActionType.TakeEquipment:
                    Add(new TilePosition(reader));
                    Add(reader.ReadByte());
                    break;
                case InputActionType.UseItem:
                    Add(new MapPosition(reader));
                    break;
                case InputActionType.UseArtilleryRemote:
                    Add(new MapPosition(reader));
                    break;
                case InputActionType.SetInventoryBar:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.ChangeActiveItemGroupForFilters:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.MoveOnZoom:
                    Add(new Vector(reader));
                    break;
                case InputActionType.StartRepair:
                    Add(new MapPosition(reader));
                    break;
                case InputActionType.Deconstruct:
                    Add(new BoundingBox(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.Upgrade:
                    Add(new BoundingBox(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.Copy:
                    Add(new BoundingBox(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.AlternativeCopy:
                    Add(new BoundingBox(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SelectBlueprintEntities:
                    Add(new BoundingBox(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.AltSelectBlueprintEntities:
                    Add(new BoundingBox(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SetupBlueprint:
                    Add(new SetupBlueprintData(reader));
                    break;
                case InputActionType.SetupSingleBlueprintRecord:
                    Add(new SetupBlueprintData(reader));
                    break;
                case InputActionType.SetSingleBlueprintRecordIcon:
                    Add(new SignalId(reader));
                    Add(reader.ReadByte());
                    break;
                case InputActionType.OpenBlueprintRecord:
                    Add(new BlueprintRecordId(reader));
                    break;
                case InputActionType.CloseBlueprintBook:
                    Add(new BlueprintRecordId(reader));
                    break;
                case InputActionType.ChangeSingleBlueprintRecordLabel:
                    Add(reader.ReadFactorioString());
                    break;
                case InputActionType.GrabBlueprintRecord:
                    Add(new BlueprintRecordId(reader));
                    break;
                case InputActionType.DropBlueprintRecord:
                    Add(reader.ReadUInt16());
                    Add(new BlueprintRecordId(reader));
                    break;
                case InputActionType.DeleteBlueprintRecord:
                    Add(new BlueprintRecordId(reader));
                    break;
                case InputActionType.CreateBlueprintLike:
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.CreateBlueprintLikeStackTransfer:
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.UpdateBlueprintShelf:
                    Add(new UpdateBlueprintShelfData(reader));
                    break;
                case InputActionType.TransferBlueprint:
                    Add(new BlueprintRecordId(reader));
                    Add(reader.ReadString());
                    break;
                case InputActionType.TransferBlueprintImmediately:
                    Add(new BlueprintRecordId(reader));
                    Add(reader.ReadString());
                    break;
                case InputActionType.ChangeBlueprintBookRecordLabel:
                    Add(new BlueprintRecordId(reader));
                    Add(reader.ReadString());
                    break;
                case InputActionType.RemoveCables:
                    Add(new MapPosition(reader));
                    break;
                case InputActionType.ExportBlueprint:
                    Add(reader.ReadUInt16());
                    Add(new BlueprintRecordId(reader));
                    break;
                case InputActionType.ImportBlueprint:
                    Add(new BlueprintRecordId(reader));
                    break;
                case InputActionType.PlayerJoinGame:
                    Add(new PlayerJoinGameData(reader));
                    break;
                case InputActionType.CancelDeconstruct:
                    Add(new BoundingBox(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.CancelUpgrade:
                    Add(new BoundingBox(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.ChangeArithmeticCombinatorParameters:
                    Add(new ArithmeticCombinatorParameters(reader));
                    break;
                case InputActionType.ChangeDeciderCombinatorParameters:
                    Add(new DeciderCombinatorParameters(reader));
                    break;
                case InputActionType.ChangeProgrammableSpeakerParameters:
                    Add(reader.ReadUInt64());
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.ChangeProgrammableSpeakerAlertParameters:
                    Add(new ProgrammableSpeakerAlertParameters(reader));
                    break;
                case InputActionType.ChangeProgrammableSpeakerCircuitParameters:
                    Add(reader.ReadByte());
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.BuildTerrain:
                    Add(new BuildTerrainParameters(reader));
                    break;
                case InputActionType.ChangeTrainWaitCondition:
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.ChangeTrainWaitConditionData:
                    Add(new TrainWaitConditionData(reader));
                    break;
                case InputActionType.CustomInput:
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.ChangeItemLabel:
                    Add(reader.ReadFactorioString());
                    break;
                case InputActionType.BuildRail:
                    Add(reader.ReadByte());
                    Add(new TilePosition(reader));
                    Add(new Direction(reader));
                    Add(new ExtendedBitBuffer(reader));
                    Add(reader.ReadByte());
                    break;
                case InputActionType.CancelResearch:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SelectArea:
                    Add(new BoundingBox(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.AltSelectArea:
                    Add(new BoundingBox(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.ServerCommand:
                    Add(reader.ReadFactorioString());
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt64());
                    break;
                case InputActionType.ClearSelectedBlueprint:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.ClearSelectedDeconstructionItem:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.ClearSelectedUpgradeItem:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.SetLogisticTrashFilterItem:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SetInfinityContainerFilterItem:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SetInfinityPipeFilter:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    Add(reader.ReadUInt64());
                    Add(reader.ReadUInt64());
                    break;
                case InputActionType.ModSettingsChanged:
                    var modSettingsCount = reader.ReadUInt32();
                    var modSettings = new ModSetting[modSettingsCount];
                    for (var i = 0; i < modSettingsCount; i++)
                        modSettings[i] = new ModSetting(reader);
                    Add(modSettings);
                    break;
                case InputActionType.SetEntityEnergyProperty:
                    Add(reader.ReadByte());
                    Add(reader.ReadUInt64());
                    break;
                case InputActionType.EditCustomTag:
                    Add(reader.ReadUInt64());
                    Add(reader.ReadFactorioString());
                    Add(new SignalId(reader));
                    Add(new MapPosition(reader));
                    break;
                case InputActionType.EditPermissionGroup:
                    Add(new EditPermissionGroupParameters(reader));
                    break;
                case InputActionType.ImportBlueprintString:
                    Add(new ImportBlueprintStringData(reader));
                    break;
                case InputActionType.ImportPermissionsString:
                    Add(reader.ReadFactorioString());
                    break;
                case InputActionType.ReloadScript:
                    Add(reader.ReadFactorioString());
                    break;
                case InputActionType.ReloadScriptDataTooLarge:
                    Add(reader.ReadUInt32());
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.GuiElemChanged:
                    Add(new GuiChangedData(reader));
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt16());
                    Add(new SignalId(reader));
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.BlueprintTransferQueueUpdate:
                    Add(reader.ReadArray(x =>
                        new Tuple<uint, uint>(x.ReadUInt32(), x.ReadUInt32())));
                    break;
                case InputActionType.DragTrainSchedule:
                    Add(new TilePosition(reader));
                    break;
                case InputActionType.DragTrainWaitCondition:
                    Add(new TilePosition(reader));
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SelectItem:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.SelectEntitySlot:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.SelectTileSlot:
                    Add(reader.ReadByte());
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.SelectMapperSlot:
                    Add(new SelectMapperSlotParameters(reader));
                    break;
                case InputActionType.DisplayResolutionChanged:
                    Add(new TilePosition(reader));
                    break;
                case InputActionType.QuickBarSetSlot:
                    Add(reader.ReadUInt16());
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.QuickBarPickSlot:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.QuickBarSetSelectedPage:
                    Add(reader.ReadByte());
                    Add(reader.ReadByte());
                    break;
                case InputActionType.PlayerLeaveGame:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.MapEditorAction:
                    throw new NotImplementedException("TODO EditorAction::loadAction");
                case InputActionType.PutSpecialItemInMap:
                    Add(new ItemStackTargetSpecification(reader));
                    break;
                case InputActionType.ChangeMultiplayerConfig:
                    Add(new MultiplayerConfigSettings(reader));
                    break;
                case InputActionType.AdminAction:
                    Add(new AdminAction(reader));
                    break;
                case InputActionType.LuaShortcut:
                    Add(reader.ReadUInt16());
                    Add(reader.ReadFactorioString());
                    break;
                case InputActionType.ChangePickingState:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SelectedEntityChangedVeryClose:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SelectedEntityChangedVeryClosePrecise:
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.SelectedEntityChangedRelative:
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SelectedEntityChangedBasedOnUnitNumber:
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SetAutosortInventory:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SetAutoLaunchRocket:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SwitchConstantCombinatorState:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SwitchPowerSwitchState:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SwitchInserterFilterModeState:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SwitchConnectToLogisticNetwork:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SetBehaviorMode:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.FastEntityTransfer:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.RotateEntity:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.FastEntitySplit:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SetTrainStopped:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.ChangeControllerSpeed:
                    Add(reader.ReadUInt64());
                    break;
                case InputActionType.SetAllowCommands:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SetResearchFinishedStopsGame:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SetInserterMaxStackSize:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.OpenTrainGui:
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SetEntityColor:
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SetDeconstructionItemTreesAndRocksOnly:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SetDeconstructionItemTileSelectionMode:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.DropToBlueprintBook:
                    Add(reader.ReadUInt16());
                    break;
                case InputActionType.DeleteCustomTag:
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.DeletePermissionGroup:
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.AddPermissionGroup:
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SetInfinityContainerRemoveUnfilteredItems:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SetCarWeaponsControl:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.SetRequestFromBuffers:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.ChangeActiveQuickBar:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.OpenPermissionsGui:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.DisplayScaleChanged:
                    Add(reader.ReadUInt64());
                    break;
                case InputActionType.SetSplitterPriority:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.GrabInternalBlueprintFromText:
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.SetHeatInterfaceTemperature:
                    Add(reader.ReadUInt64());
                    break;
                case InputActionType.SetHeatInterfaceMode:
                    Add(reader.ReadByte());
                    break;
                case InputActionType.OpenTrainStationGui:
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.RemoveTrainStation:
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.GoToTrainStation:
                    Add(reader.ReadUInt32());
                    break;
                case InputActionType.RenderModeChanged:
                    Add(reader.ReadByte());
                    break;
                default:
                    throw new Exception($"No such InputAction: 0x{Action}");
            }

            return this;
        }

        private void Add(object obj)
        {
            Data.Add(obj);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((byte)Action);
            writer.WriteVarShort(PlayerIndex);
            foreach (var obj in Data)
            {
                if (obj is IWritable<object> writable)
                    writable.Write(writer);
                else if (obj is uint uint32)
                    writer.Write(uint32);
                else if (obj is byte int8)
                    writer.Write(int8);
                else if (obj is ushort uint16)
                    writer.Write(uint16);
                else if (obj is ulong uint64)
                    writer.Write(uint64);
                else if (obj is string str)
                {
                    if (Action == InputActionType.TransferBlueprint
                        || Action == InputActionType.TransferBlueprintImmediately
                        || Action == InputActionType.ChangeBlueprintBookRecordLabel)
                        writer.Write(str);
                    else
                        writer.WriteFactorioString(str);
                }
                else
                    throw new Exception($"Unsopported object in Data list: {obj.ToString()}");
            }
        }
    }
}