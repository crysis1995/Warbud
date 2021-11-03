using System;
using System.Threading.Tasks;
using FluentValidation;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using Warbud.Users.Database.Models;
using Warbud.Users.Infrastructure.Data;
using Warbud.Users.Types.Inputs;
using Warbud.Users.Types.Payloads;
using Warbud.Shared.Constants;
using Warbud.Users.Helpers;

namespace Warbud.Users.GqlControllers
{
    public partial class Mutation
    {
        [Authorize(Roles = new []{Role.Name.Admin})]
        [UseDbContext(typeof(UserDbContext))]
        public async Task<WarbudAppPayload> AddAppAsync(AddWarbudAppInput input, [ScopedService] UserDbContext context)
        {
            await _appValidator.ValidateAndThrowAsync(input);

            var (appName, moduleName) = input;
            var app = new WarbudApp()
            {
                AppName = appName,
                ModuleName = moduleName
            };

            context.WarbudApps.Add(app);
            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
                return new WarbudAppPayload(app);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to add new app: {ex.Message}", ex);
            }
        }
        
        [Authorize(Roles = new []{ Role.Name.Admin})]
        [UseDbContext(typeof(UserDbContext))]
        public async Task<WarbudAppPayload> UpdateAppAsync(UpdateWarbudAppInput input, [ScopedService] UserDbContext context)
        {
            var app =await  context.WarbudApps.FindAsync(input.Id);
            if (app is null)
            {
                throw new ArgumentException("There is no app with given Id");
            }
            app.UpdateEntity(input);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return new WarbudAppPayload(app);
        }
        
        [Authorize(Roles = new []{ Role.Name.Admin})]
        [UseDbContext(typeof(UserDbContext))]
        public async Task<bool> DeleteAppAsync(int id, [ScopedService] UserDbContext context)
        {
            var app = await context.WarbudApps.FindAsync(id);
            if (app is null)
            {
                return false;
            }
            context.WarbudApps.Remove(app);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
    }
}