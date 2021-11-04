using System.Linq;
using Warbud.Shared.Interfaces;
using Warbud.Users.Database.Common;

namespace Warbud.Users.Helpers
{
    internal static class MutationHelper
    {
        public static void UpdateEntity<TEntity, TInput>(this TEntity entity, TInput input) where TEntity : IEntity
        {
            foreach (var prop in input.GetType().GetProperties().Where(x => x.GetValue(input) is not null))
            {
                entity.GetType().GetProperty(prop.Name).SetValue(entity, prop.GetValue(input));
            }
        }
    }
}