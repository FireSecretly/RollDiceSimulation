namespace RollDiceSimulation.Models;

public class DiceRoll
{
    public int Id { get; set; }
    public int NumberOfDice { get; private set; }
    public List<DiceRollResults> Results { get; set; }
    public int Total => Results.Sum(r => r.Result);

    public DiceRoll(int numberOfDice)
    {
        NumberOfDice = numberOfDice;
        Results = new List<DiceRollResults>();
    }
}