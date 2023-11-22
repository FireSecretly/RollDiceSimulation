using System.ComponentModel.DataAnnotations;

namespace RollDiceSimulation.Models;

public class DiceSettings
{
    [Range(1, 6, ErrorMessage = "Favored face must be between 1 and 6.")]
    public int FavoredFaceDie1 { get; set; }

    [Range(1, 5, ErrorMessage = "Factor must be between 1 and 5.")]
    public int FactorDie1 { get; set; }

    [Range(1, 6, ErrorMessage = "Favored face must be between 1 and 6.")]
    public int FavoredFaceDie2 { get; set; }

    [Range(1, 5, ErrorMessage = "Factor must be between 1 and 5.")]
    public int FactorDie2 { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Number of rolls must be at least 1.")]
    public int NumberOfRolls { get; set; }
}