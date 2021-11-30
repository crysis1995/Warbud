namespace Warbud.Revit.Statistics.ValueObjects
{
    internal record OperationData(string AppName, string OperationName, long OperationTimeMs, int OperationAmount);
}