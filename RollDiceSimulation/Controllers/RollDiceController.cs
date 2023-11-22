using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RollDiceSimulation.Data;
using RollDiceSimulation.Models;
using RollDiceSimulation.Services;

namespace RollDiceSimulation.Controllers;

public class RollDiceController : Controller
{
    private readonly IDiceRollerService _diceRollerService;
    private readonly ILogger<RollDiceController> _logger;
    private readonly RollDiceDbContext _context;

    public RollDiceController (IDiceRollerService diceRollerService, RollDiceDbContext context, ILogger<RollDiceController> logger)
    {
        _diceRollerService = diceRollerService;
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Roll(DiceSettings settings)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", settings);
        }

        try
        {
            await _diceRollerService.RollDiceAsync(settings);
            return RedirectToAction(nameof(ShowRolls));
        }
        catch (Exception ex)
        {              
            _logger.LogError(ex, "An error occurred while processing the dice roll");
            TempData["ErrorMessage"] = "An error occurred while rolling the dice.";
            return RedirectToAction(nameof(Index));
        }
    }

    public async Task<IActionResult> ShowRolls()
    {
        try
        {
            var viewModel = new DiceRollViewModel
            {
                DiceRolls = await _context.DiceRolls!.Include(dr => dr.Results).ToListAsync()
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving dice rolls");
            return View("Error");
        }
    }
}