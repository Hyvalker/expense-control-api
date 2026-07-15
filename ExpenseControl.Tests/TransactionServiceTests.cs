using Xunit;
using Moq; 
using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Enums;
using ExpenseControl.Core.Interfaces;
using ExpenseControl.Core.Services;

namespace ExpenseControl.Tests;

public class TransactionServiceTests
{
    [Fact]
    public async Task CreateAsync_Should_Throw_InvalidOperationException_When_Minor_Registers_Income()
    {
        // Arrange
        var mockTransactionRepo = new Mock<ITransactionRepository>();
        var mockPersonRepo = new Mock<IPersonRepository>();
        
        var service = new TransactionService(mockTransactionRepo.Object, mockPersonRepo.Object);
        
        var person = new Person { Id = 1, Age = 17 }; // Menor de idade
        mockPersonRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(person);
        
        var transaction = new Transaction { PersonId = 1, Type = TransactionType.Income, Amount = 100 };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => service.CreateAsync(transaction));
    }
}