using ProjectMGN.DTOS.Request;
using ProjectMGN.Models;

namespace ProjectMGN.Interfaces.Services;

public interface IActionService
{
    public void CreateAction(CreateActionRequest action);
    public List<Actions> GetActions(int ownerId);
    public Actions GetAction(int actionId);
    public void UpdateAction(Actions updatedAction);
    public void DeleteAction(int actionId);
}