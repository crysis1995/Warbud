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
        [Authorize(Roles = new []{ Role.Name.Admin})]
        [UseDbContext(typeof(UserDbContext))]
        public async Task<WarbudClaimPayload> AddClaimAsync(AddWarbudClaimInput input,
            [ScopedService] UserDbContext context)
        {
            var (userId, appId, projectId, name) = input;
            var claimCheck = await context.WarbudClaims.FindAsync(userId, appId, projectId);
            if (claimCheck is not null) throw new ArgumentException("There is claim with this ids");
            
            await _claimValidator.ValidateAndThrowAsync(input);
            
            var claim = new WarbudClaim(userId, appId, projectId, name);
            context.WarbudClaims.Add(claim);
            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
                return new WarbudClaimPayload(claim);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to add new claim: {ex.Message}", ex);
            }
        }

        [Authorize(Roles = new []{Role.Name.Admin})]
        [UseDbContext(typeof(UserDbContext))]
        public async Task<WarbudClaimPayload> UpdateClaimAsync(UpdateWarbudClaimInput input,
            [ScopedService] UserDbContext context)
        {
            var (userId, appId, projectId, name) = input;

            var claim = await context.WarbudClaims.FindAsync(userId, appId, projectId);
            if (claim is null) throw new ArgumentException("There is no claim with given Id");
            claim.UpdateEntity(input);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return new WarbudClaimPayload(claim);
        }

        [Authorize(Roles = new []{ Role.Name.Admin})]
        [UseDbContext(typeof(UserDbContext))]
        public async Task<bool> DeleteClaimAsync(DeleteWarbudClaimInput input, [ScopedService] UserDbContext context)
        {
            var (userId, appId, projectId) = input;
            var claim = await context.WarbudClaims.FindAsync(userId, appId, projectId);
            if (claim is null) return false;
            context.WarbudClaims.Remove(claim);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
    }
}