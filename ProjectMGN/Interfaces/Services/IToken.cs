using ProjectMGN.DTOS.Request;
using ProjectMGN.Models;

namespace ProjectMGN.Interfaces.Services
{
    public interface IToken
    {
        string GenerateToken(User user);
        int? ValidateToken(string token);
        int UserIdFromToken(string token);
    }
}
