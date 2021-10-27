namespace Warbud.Users.Types.Inputs
{
    public record AddWarbudAppInput(string AppName, string ModuleName);

#nullable enable
    public record UpdateWarbudAppInput(int Id, string? AppName = null, string? ModuleName = null);
#nullable disable
}