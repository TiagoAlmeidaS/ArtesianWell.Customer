namespace Customer.Application.Usecases.Customer.Query.GetCustomer;

public class GetCustomerResult
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int ProfileType { get; set; }
    public string Document { get; set; }
}