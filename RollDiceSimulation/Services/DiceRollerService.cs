using RollDiceSimulation.Data;
using RollDiceSimulation.Models;

namespace RollDiceSimulation.Services;

public class DiceRollerService : IDiceRollerService
{
    private readonly RollDiceDbContext _context;
    private readonly ILogger<DiceRollerService> _logger;
    private readonly Random _random = new();

    public DiceRollerService(RollDiceDbContext context, ILogger<DiceRollerService> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<List<DiceRoll>> RollDiceAsync(DiceSettings settings)
    {
        var rolls = new List<DiceRoll>();

        for (var i = 0; i < settings.NumberOfRolls; i++)
        {
            var diceRoll = new DiceRoll(2); // Assuming 2 dice

            // Roll each die and store the results
            for (var j = 0; j < diceRoll.NumberOfDice; j++)
            {
                var favoredFace = (j == 0) ? settings.FavoredFaceDie1 : settings.FavoredFaceDie2;
                var factor = (j == 0) ? settings.FactorDie1 : settings.FactorDie2;
                var result = RollDie(favoredFace, factor);
                diceRoll.Results.Add(new DiceRollResults { Result = result });
            }

            rolls.Add(diceRoll);
        }

        try
        {
            await _context.DiceRolls!.AddRangeAsync(rolls);
            await _context.SaveChangesAsync();
            return rolls;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while rolling dice");
            throw;  //  In this case, rethrow the caught exception and let it propagate to the controller.
        }
    }

    private int RollDie(int favoredFace, int factor)
    {
        var totalChances = 6 + (factor - 1);
        var roll = _random.Next(1, totalChances + 1);

        return roll > 6 ? favoredFace : roll;
    }
}