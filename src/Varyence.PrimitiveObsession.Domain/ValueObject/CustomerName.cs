using CSharpFunctionalExtensions;

namespace Varyence.PrimitiveObsession.Domain.ValueObject
{
    public sealed class CustomerName
    {
        private readonly string _value;

        private CustomerName(string name) => 
            _value = name;

        public static Result<CustomerName> Create(string name) =>
            name switch
            {
                _ when string.IsNullOrWhiteSpace(name) => 
                    Result.Failure<CustomerName>("Name can't be empty"),
                
                _ when name.Length > 50 =>  
                    Result.Failure<CustomerName>("Name is too long"),
                
                _ when name.Length < 3 =>
                    Result.Failure<CustomerName>("Name is too short"),
                
                _ => Result.Success(new CustomerName(name))
            };
 
        public static implicit operator string(CustomerName email) => 
            email._value;

        public override bool Equals(object obj) =>
            obj switch
            {
                CustomerName email => _value == email._value,
                _ => false
            };

        public static bool operator ==(CustomerName a, CustomerName b) =>
            ReferenceEquals(a, null) && ReferenceEquals(b, null) || 
            !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Equals(b);

        public static bool operator !=(CustomerName a, CustomerName b) => 
            !(a == b);

        public override int GetHashCode() => 
            _value.GetHashCode();
    }
}