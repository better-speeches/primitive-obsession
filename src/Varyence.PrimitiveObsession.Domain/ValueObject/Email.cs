using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace Varyence.PrimitiveObsession.Domain.ValueObject
{
    public struct Email
    {
        private readonly string _value;
 
        private Email(string value) => 
            _value = value;

        public static Result<Email> Create(string email) =>
            email switch
            {
                _ when string.IsNullOrWhiteSpace(email) =>
                    Result.Failure<Email>("E-mail can't be empty"),

                _ when email.Length > 100 =>
                    Result.Failure<Email>("E-mail is too long"),

                _ when !Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") =>
                    Result.Failure<Email>("E-mail is invalid"),

                _ => Result.Success(new Email(email))
            };
 
        public static implicit operator string(Email email) => 
            email._value;

        public static explicit operator Email(string email) =>
            Email.Create(email).Value;

        public override bool Equals(object obj) =>
            obj switch
            {
                Email email => _value == email._value,
                _ => false
            };

        public static bool operator ==(Email a, Email b) =>
            ReferenceEquals(a, null) && ReferenceEquals(b, null) || 
            !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Equals(b);

        public static bool operator !=(Email a, Email b) => 
            !(a == b);

        public override int GetHashCode() => 
            _value.GetHashCode();
    }
}