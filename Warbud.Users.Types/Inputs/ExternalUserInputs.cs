using System;
using Warbud.Users.Database.Common;

namespace Warbud.Users.Types.Inputs
{
    public record AddExternalUserInput(string FirstName, string LastName, string Password, string Email, string ConfirmPassword);
    public record LoginExternalUserInput(string Email, string Password);
    
    /// <summary>
    /// Input for admin usage
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Role"></param>
    public record UpdateUserRoleInput(Guid Id, Role Role);
    
    /// <summary>
    /// Input for user personal information
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
#nullable enable
    public record UpdateExternalUserInput(Guid Id, string? FirstName = null, string? LastName = null, string? Email = null);
#nullable disable
}