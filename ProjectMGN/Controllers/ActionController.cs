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
   private readonly IToken _tokenService;

   public ActionController(IActionService actionService,IToken tokenService)
   {
      _actionService = actionService;
      _tokenService = tokenService;
   }

   [Authorize]
   [HttpGet("getActions")]
   public IActionResult GetActions()
   {
      string token = HttpContext.Request.Headers.Authorization;
      var cleanToken = token.Split(" ")[1];
      var ownerId = _tokenService.UserIdFromToken(cleanToken);
      var data = _actionService.GetActions(ownerId);
      return Ok(new {data});
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