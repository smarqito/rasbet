using BetApplication.Interfaces;
using BetFacade;
using DTO.BetDTO;
using Microsoft.AspNetCore.Mvc;

namespace BetAPI.Controllers;

public class SelectionController : Controller
{
    private readonly IBetFacade _betFacade;

    public SelectionController(IBetFacade betFacade)
    {
        _betFacade = betFacade;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSelection()
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> GetSelectionsByGame()
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> GetSelectionsByType() 
    { 
        throw new NotImplementedException(); 
    }
}
