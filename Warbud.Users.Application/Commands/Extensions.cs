using System.Linq;
using Warbud.Shared.Interfaces;

namespace Warbud.Users.Application.Commands
{
    internal static class Extensions
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