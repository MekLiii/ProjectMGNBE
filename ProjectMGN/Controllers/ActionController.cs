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
   private readonly IHttpContextAccessor _httpContextAccessor;

   public ActionController(IActionService actionService,IHttpContextAccessor httpContextAccessor)
   {
      _actionService = actionService;
      _httpContextAccessor = httpContextAccessor;
   }

   [Authorize]
   [HttpGet("getActions")]
   public IActionResult GetActions()
   {
      int ownerId = 1;
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