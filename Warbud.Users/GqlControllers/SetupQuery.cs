using Microsoft.AspNetCore.Identity;
using Warbud.Users.Authentication;
using Warbud.Users.Database.Models;
using Warbud.Users.Services;

namespace Warbud.Users.GqlControllers
{
    public partial class Query
    {
        private readonly IPasswordHasher<ExternalUser> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUserContextService _userContextService;

        public Query(IPasswordHasher<ExternalUser> passwordHasher,
            AuthenticationSettings authenticationSettings,
            IUserContextService userContextService)
        {
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _userContextService = userContextService;
        }
    }
}