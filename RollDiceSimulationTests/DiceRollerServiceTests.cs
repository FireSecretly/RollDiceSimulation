using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RollDiceSimulation.Data;
using RollDiceSimulation.Models;
using RollDiceSimulation.Services;

namespace RollDiceSimulationTests;

public class DiceRollerServiceTests
{
    private RollDiceDbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<RollDiceDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        return new RollDiceDbContext(options);
    }

    private DiceRollerService CreateService(RollDiceDbContext context)
    {
        var loggerMock = new Mock<ILogger<DiceRollerService>>();
        return new DiceRollerService(context, loggerMock.Object);
    }

    [Fact]
    public async Task RollDiceAsync_ReturnsCorrectNumberOfRolls()
    {
        // Arrange
        var context = CreateDbContext("RollDiceDb1");
        var service = CreateService(context);
        var settings = new DiceSettings { NumberOfRolls = 5, FavoredFaceDie1 = 2, FactorDie1 = 1, FavoredFaceDie2 = 3, FactorDie2 = 2 };

        // Act
        var result = await service.RollDiceAsync(settings);

        // Assert
        Assert.Equal(settings.NumberOfRolls, result.Count);
    }

    [Fact]
    public async Task RollDiceAsync_AllRollsHaveResults()
    {
        // Arrange
        var context = CreateDbContext("RollDiceDb2");
        var service = CreateService(context);
        var settings = new DiceSettings { NumberOfRolls = 3, FavoredFaceDie1 = 2, FactorDie1 = 1, FavoredFaceDie2 = 3, FactorDie2 = 2 };

        // Act
        var result = await service.RollDiceAsync(settings);

        // Assert
        Assert.All(result, diceRoll => Assert.Equal(2, diceRoll.Results.Count));
    }
}