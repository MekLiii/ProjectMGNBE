using ProjectMGN.DTOS.Request;
using ProjectMGN.Models;

namespace ProjectMGN.Interfaces.Repositories;

public interface IActionRepository
{
    public void CreateAction(Actions action);
    public List<Actions> GetActions(int ownerId);
    public Actions GetAction(int actionId);
    public void UpdateAction(Actions updatedAction);
    public void DeleteAction(int actionId);
}