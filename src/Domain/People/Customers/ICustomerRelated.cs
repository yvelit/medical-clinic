namespace Domain.People.Customers
{
    public interface ICustomerRelated
    {
        Cpf CustomerCode { get; }
        Customer Customer { get; }
    }
}
