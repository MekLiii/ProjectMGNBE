using ProjectMGN.DTOS.Request;
using ProjectMGN.Interfaces.Repositories;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;

namespace ProjectMGN.Services;

public class ActionService : IActionService
{
    private readonly IActionRepository _actionRepository;

    public ActionService(IActionRepository actionService)
    {
        _actionRepository = actionService;
    }

    public void CreateAction(CreateActionRequest action)
    {
        Actions newAction = new()
        {
            ConfigurationId = action.ConfigurationId,
            ActionName = action.ActionName,
            CommandId = action.CommandId,
            OwnerId = action.OwnerId
            
        };
        _actionRepository.CreateAction(newAction);
    }

    public List<Actions> GetActions(int ownerId)
    {
        return _actionRepository.GetActions(ownerId);
    }

    public Actions GetAction(int actionId)
    {
        return _actionRepository.GetAction(actionId);
    }

    public void UpdateAction(Actions updatedAction)
    {
        _actionRepository.UpdateAction(updatedAction);
    }

    public void DeleteAction(int actionId)
    {
        _actionRepository.DeleteAction(actionId);
    }
}