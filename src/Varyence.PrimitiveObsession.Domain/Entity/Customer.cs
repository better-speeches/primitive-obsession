using Varyence.PrimitiveObsession.Domain.ValueObject;

namespace Varyence.PrimitiveObsession.Domain.Entity
{
    public sealed class Customer
    {
        public Customer(Email email, CustomerName name) => 
            (Email, Name) = (email, name);

        public CustomerName Name { get; private set; }
        public Email Email { get; private set; }

        public void ChangeName(CustomerName name) =>
            Name = name;

        public void ChangeEmail(Email email) =>
            Email = email;
    }
}