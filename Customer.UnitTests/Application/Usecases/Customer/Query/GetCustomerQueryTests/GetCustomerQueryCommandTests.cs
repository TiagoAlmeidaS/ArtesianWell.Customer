using System.Linq.Expressions;
using Customer.Application.Usecases.Customer.Query.GetCustomer;
using Customer.Domain.Entities;
using Customer.Domain.Repositories;
using Shared.Messages;

namespace Customer.UnitTests.Application.Usecases.Customer.Query.GetCustomerQueryTests;

using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;
using Moq;


public class GetCustomerQueryHandlerTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IMessageHandlerService> _messageHandlerServiceMock;
    private readonly GetCustomerQueryHandler _handler;

    public GetCustomerQueryHandlerTests()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _messageHandlerServiceMock = new Mock<IMessageHandlerService>();
        _handler = new GetCustomerQueryHandler(_customerRepositoryMock.Object, _messageHandlerServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ValidQuery_ShouldReturnSuccessResult()
    {
        // Arrange
        var query = new GetCustomerQuery
        {
            Document = Faker.NumberFaker.Number(11).ToString(),
            Email = Faker.InternetFaker.Email()
        };

        var customerEntity = new List<CustomerEntity>
        {
            new CustomerEntity
            {
                Document = query.Document,
                Email = query.Email,
                // Add other properties as needed
            }
        };

        _customerRepositoryMock.Setup(x => x.GetWhere(It.IsAny<Expression<Func<CustomerEntity, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customerEntity);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        // Add more assertions based on the properties of GetCustomerResult
    }
}