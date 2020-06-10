namespace Domain.People.Customers
{
    public interface ICustomerRelated
    {
        //Interface que relaciona um cpf a um cliente
        Cpf CustomerCode { get; }
        Customer Customer { get; }
    }
}
