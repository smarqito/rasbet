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
