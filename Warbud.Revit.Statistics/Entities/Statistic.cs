#nullable enable
using System;
using Warbud.Revit.Statistics.ValueObjects;
using Warbud.Shared.Interfaces;

namespace Warbud.Revit.Statistics.Entities
{
    public class Statistic : IEntity
    {
        private Statistic()
        {
            
        }
        
        internal Statistic(UserByVariables userByVariables,
            OperationData operationData)
        {
            UserByVariables = userByVariables;
            OperationData = operationData;
        }

        public int Id { get; private  set;}
        internal UserByVariables UserByVariables { get; private set;}
        internal OperationData OperationData { get; private set; }
        internal DateTime DateTime { get; private set; } = DateTime.Now;
    }
}