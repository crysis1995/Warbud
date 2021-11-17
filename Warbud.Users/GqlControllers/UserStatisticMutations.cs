using System;
using System.Threading.Tasks;
using FluentValidation;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using Warbud.Shared.Constants;
using Warbud.Users.Database.Models;
using Warbud.Users.Helpers;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;
using Warbud.Users.Types.Payloads;

namespace Warbud.Users.GqlControllers
{
    public partial class Mutation
    {
        [UseDbContext(typeof(UserDbContext))]
        public async Task<bool> LogStatisticByVariableAsync(UserStatisticByVariableInput input,
            [ScopedService] UserDbContext context)
        {
            var ( userName,  userDomainName,  computerName,  appName,  operationName,  time,  amount) = input;
            var userStatistic = new UserStatistic(userName, userDomainName, computerName, appName, operationName, time, amount);
            context.UserStatistics.Add(userStatistic);
            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to log action", ex);
            }
        }
        
        [UseDbContext(typeof(UserDbContext))]
        public async Task<bool> LogStatisticByUserIdAsync(UserStatisticByUserIdInput input,
            [ScopedService] UserDbContext context)
        {
            var (  userId,  appName,  operationName) = input;
            var userStatistic = new UserStatistic(userId,  appName,  operationName);
            context.UserStatistics.Add(userStatistic);
            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to log action", ex);
            }
        }
    }
}