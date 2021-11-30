using System;

namespace Warbud.Revit.Statistics.DTO
{
    public record StatisticDto(int Id, string User, string AppName, string OperationName, long OperationTimeMs, int OperationAmount, DateTime DateTime);
}