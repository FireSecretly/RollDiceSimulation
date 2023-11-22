using RollDiceSimulation.Models;

namespace RollDiceSimulation.Services;

public interface IDiceRollerService
{
    Task<List<DiceRoll>> RollDiceAsync(DiceSettings settings);
}