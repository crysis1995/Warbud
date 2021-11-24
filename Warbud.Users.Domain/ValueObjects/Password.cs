using Warbud.Users.Domain.Exceptions;

namespace Warbud.Users.Domain.ValueObjects
{
    public class Password
    {
        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyPasswordException();
            }
            Value = value;
        }
        public string Value { get; }
        
        public static implicit operator string(Password email)
            => email.Value;
        
        public static implicit operator Password(string email)
            => new(email);
    }
}