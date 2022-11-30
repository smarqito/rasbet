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

    [HttpGet("statistics")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StatisticsDTO))]
    public async Task<IActionResult> GetStatisticsByGame([FromQuery] ICollection<int> oddIds)
    {
        try
        {
            StatisticsDTO dto = await _betFacade.GetStatisticsByGame(oddIds);
            return Ok(dto);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

}
