using BetFacade;
using Domain;
using DTO.BetDTO;
using Microsoft.AspNetCore.Mvc;

namespace BetAPI.Controllers;

public class SelectionController : BaseController
{
    private readonly IBetFacade _betFacade;

    public SelectionController(IBetFacade betFacade)
    {
        _betFacade = betFacade;
    }

    [HttpGet]
    public async Task<IActionResult> teste()
    {
        APIService APIService = new ();
        var s = await APIService.GetOdd(1, 1);
        return Ok(s);
    }

    [HttpPost("selection")]
    public async Task<IActionResult> CreateSelection([FromBody] CreateSelectionDTO create)
    {
        try
        {
            Selection s = await _betFacade.CreateSelection(create.BetTypeId, create.OddId, create.odd);
            return Ok(s);
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("game")]
    public async Task<IActionResult> GetSelectionsByGame([FromBody] GetSelectionDTO get)
    {
        try
        {
            ICollection<Selection> selections = await _betFacade.GetSelectionByGame(get.Id);
            return Ok(selections);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("bettype")]
    public async Task<IActionResult> GetSelectionsByType([FromBody] GetSelectionDTO get) 
    {
        try
        {
            ICollection<Selection> selections = await _betFacade.GetSelectionByType(get.Id);
            return Ok(selections);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
