using ProjectMGN.Db;
using ProjectMGN.DTOS.Request;
using ProjectMGN.Interfaces.Repositories;
using ProjectMGN.Models;

namespace ProjectMGN.Repository;

public class ActionRepository : IActionRepository
{
    private readonly ProjectMGNDB _projectMgndb;

    public ActionRepository(ProjectMGNDB projectMgndb)
    {
        _projectMgndb = projectMgndb;
    }

    public void CreateAction(Actions action)
    {
        _projectMgndb.Actions.Add(action);
        _projectMgndb.SaveChanges();
    }

    public List<Actions> GetActions(int ownerId)
    {
        var actions = _projectMgndb.Actions.Where(action => action.OwnerId == ownerId).ToList();
        return actions;
    }

    public Actions GetAction(int actionId)
    {
        var actions = _projectMgndb.Actions.FirstOrDefault(action => action.OwnerId == actionId);
        if (actions == null) throw new AggregateException("Action not found");
        return actions;
    }

    public void UpdateAction(Actions updatedAction)
    {
        var existingAction = _projectMgndb.Actions.FirstOrDefault(action => action.Id == updatedAction.Id);
        if (existingAction == null)
        {
            throw new AggregateException("Action not found");
        }

        existingAction.ActionName = updatedAction.ActionName;
        existingAction.CommandId = updatedAction.CommandId;
        existingAction.ConfigurationId = updatedAction.ConfigurationId;
        _projectMgndb.SaveChanges();
    }

    public void DeleteAction(int actionId)
    {
        var action = _projectMgndb.Actions.FirstOrDefault(action => action.Id == actionId);
        if (action == null) throw new ArgumentException("Action not found");
        _projectMgndb.Actions.Remove(action);
        _projectMgndb.SaveChanges();
    }
}