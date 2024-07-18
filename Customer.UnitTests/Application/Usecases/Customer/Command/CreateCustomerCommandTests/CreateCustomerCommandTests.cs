using Customer.Application.Usecases.Customer.Command.CreateCustomer;
using Customer.Domain.Entities;
using Customer.Domain.Repositories;
using Moq;
using Shared.Messages;
using Xunit;

namespace Customer.UnitTests.Application.Usecases.Customer.Command.CreateCustomerCommandTests;

public class CreateCustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IMessageHandlerService> _messageHandlerServiceMock;
    private readonly CreateCustomerCommandHandler _handler;

    public CreateCustomerCommandHandlerTests()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _messageHandlerServiceMock = new Mock<IMessageHandlerService>();
        _handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object, _messageHandlerServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldReturnSuccessResult()
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            Name = Faker.NameFaker.Name(),
            Email = Faker.InternetFaker.Email(),
            Phone = Faker.NumberFaker.Number().ToString(),
            Document = Faker.NumberFaker.Number(11).ToString(),
            ProfileType = Faker.NameFaker.Name()
        };

        var customerEntity = new CustomerEntity
        {
            Id = Guid.NewGuid().ToString(),
            Document = command.Document,
            Email = command.Email,
            Name = command.Name,
            Phone = command.Phone,
            ProfileTypeId = command.ProfileType
        };

        _customerRepositoryMock.Setup(x => x.Insert(It.IsAny<CustomerEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customerEntity);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        // Add more assertions based on the properties of CreateCustomerResult
    }
}
