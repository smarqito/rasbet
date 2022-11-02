using BetApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BetAPI.Controllers;

public class SelectionController : Controller
{
    public ISelectionRepository selectionRepository { get; set; }

    public SelectionController(ISelectionRepository selectionRepository)
    {
        this.selectionRepository = selectionRepository;
    }

    [HttpPost]
    public Task<IActionResult> createSelection()
    {
        throw new NotImplementedException();
    }
}
