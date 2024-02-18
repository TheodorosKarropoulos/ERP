using InterWorks.DynamicFields.Commands;
using InterWorks.DynamicFields.Constants;
using InterWorks.DynamicFields.Models;
using InterWorks.DynamicFields.Repositories.Abstractions;
using InterWorks.DynamicFields.Services;
using MediatR;
using Moq;

namespace InterWorks.DynamicFields.IntegrationTests.Services;

public class CustomerFieldServiceTests
{
    [Fact]
    public async Task CreateFieldAsync_CreatesNewFieldAndInsertsHistory()
    {
        // Arrange
        var repositoryMock = new Mock<ICustomerFieldRepository>();
        repositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<CustomerField>()))
            .Returns(Task.CompletedTask);

        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(mediator =>
                mediator.Send(It.IsAny<InsertFieldHistoryCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new FieldValueHistory
            {
                NewValue = null
            }));

        var service = new CustomerFieldService(repositoryMock.Object, mediatorMock.Object);

        var customerField = new CustomerField
        {
            CustomerId = Guid.NewGuid(),
            FieldName = "TestField",
            Value = "TestValue"
        };

        // Act
        await service.CreateFieldAsync(customerField);

        // Assert
        repositoryMock.Verify(repo => repo.CreateAsync(customerField), Times.Once);
        mediatorMock.Verify(
            mediator => mediator.Send(It.IsAny<InsertFieldHistoryCommand>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesFieldAndInsertsHistory()
    {
        // Arrange
        var fieldId = Guid.NewGuid();
        var repositoryMock = new Mock<ICustomerFieldRepository>();
        repositoryMock.Setup(repo => repo.GetByIdAsync(fieldId))
            .ReturnsAsync(new CustomerField
            {
                Id = fieldId,
                CustomerId = Guid.NewGuid(),
                FieldName = "FieldName",
                Value = "FieldValue",
                Type = FieldType.DropDownList
            });
        repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<CustomerField>()))
            .Returns(Task.CompletedTask);

        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(mediator =>
                mediator.Send(It.IsAny<InsertFieldHistoryCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new FieldValueHistory
            {
                NewValue = "FieldName"
            }));

        var service = new CustomerFieldService(repositoryMock.Object, mediatorMock.Object);
        
        // Act
        await service.UpdateAsync(fieldId, "NewValue");
        
        // Assert
        repositoryMock.Verify(repo => repo.GetByIdAsync(fieldId), Times.Once);
        repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<CustomerField>()), Times.Once);
        mediatorMock.Verify(mediator => mediator.Send(It.IsAny<InsertFieldHistoryCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}