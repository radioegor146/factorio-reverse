namespace FactorioNetParser.FactorioNet.Data
{
    internal static class InputActionTypeExtensions
    {
        public static bool IsSegmentable(this InputActionType type)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (type)
            {
                case InputActionType.WriteToConsole:
                case InputActionType.UpdateBlueprintShelf:
                case InputActionType.TransferBlueprint:
                case InputActionType.ServerCommand:
                case InputActionType.ImportBlueprintString:
                case InputActionType.ReloadScript:
                    return true;
                default:
                    return false;
            }
        }
    }
}