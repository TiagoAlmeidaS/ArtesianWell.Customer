using Customer.Domain.SeedWork;

namespace Customer.Domain.Entities;

public class CustomerEntity: AggregateRoot
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int ProfileTypeId { get; set; }
    public string Document { get; set; }
}