namespace Warbud.Users.Types.Inputs
{
    public record AddExternalUserInput(string FirstName, string LastName, string Password, string Email, string ConfirmPassword);
    public record LoginExternalUserInput(string Email, string Password);
}