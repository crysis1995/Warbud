using System;
using System.ComponentModel.DataAnnotations;

namespace Warbud.Users.Types.Inputs
{
    /// <summary>
    /// Input for user personal information
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    #nullable enable
        public record UpdateUserInput(Guid Id, string? FirstName = null, string? LastName = null);
    #nullable disable
}