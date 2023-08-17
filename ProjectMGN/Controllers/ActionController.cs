using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectMGN.DTOS.Request;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;

namespace ProjectMGN.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActionController : ControllerBase
{
   private readonly IActionService _actionService;

   public ActionController(IActionService actionService)
   {
      _actionService = actionService;
   }

   [Authorize]
   [HttpGet("getActions/{ownerId}")]
   public IActionResult GetActions(int ownerId)
   {
      return Ok(_actionService.GetActions(ownerId));
   }

   [Authorize]
   [HttpPost("createAction")]
   public IActionResult CreateAction (CreateActionRequest actions)
   {
      _actionService.CreateAction(actions);
      var message = "Action created successfully";
      return Ok(new {message});
   }
}