namespace Warbud.Users.Types.Inputs
{
    public record UserStatisticByVariableInput(string UserName, string UserDomainName, string ComputerName, string AppName, string OperationName, long OperationTime, int OperationAmount);

    public record UserStatisticByUserIdInput(int UserId, string AppName, string OperationName);


}