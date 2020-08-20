using CSharpFunctionalExtensions;
using Varyence.PrimitiveObsession.Domain.Entity;
using Varyence.PrimitiveObsession.Domain.ValueObject;
using static System.Console;

namespace Varyence.PrimitiveObsession.ConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            CreateCustomer1("test@mail.com", "leo")
                .Tap(customer => WriteLine(customer.Email))
                .Tap(customer => WriteLine(customer.Name))
                .OnFailure(error => WriteLine(error));

            var emailOne = Email.Create("valid@mail.com").Value;
            var emailTwo = Email.Create("valid@mail.com").Value;
            
            WriteLine(emailOne == emailTwo ? "Emails are equal!" : "Emails are not equal!");
            
            // Tricky thing
            WriteLine($"{emailOne}");
            WriteLine(emailOne);
        }

        private static Result<Customer> CreateCustomer1(string email, string customerName)
        {
            // Email
            Result<Email> emailResult = Email.Create(email);
            if (emailResult.IsFailure)
                return Result.Failure<Customer>(emailResult.Error);
            
            Email emailObj = emailResult.Value;

            // CustomerName
            Result<CustomerName> customerNameResult = CustomerName.Create(customerName);
            if (customerNameResult.IsFailure)
                return Result.Failure<Customer>(customerNameResult.Error);
            
            CustomerName customerNameObj = customerNameResult.Value;
            
            // Customer
            return new Customer(emailObj, customerNameObj);
        }
        
        private static Result<Customer> CreateCustomer2(string email, string customerName)
        {
            var emailResult = Email.Create(email);
            var customerNameResult = CustomerName.Create(customerName);

            return Result
                .Combine(emailResult, customerNameResult)
                .Map(() => new Customer(emailResult.Value, customerNameResult.Value));
        }
    }
}