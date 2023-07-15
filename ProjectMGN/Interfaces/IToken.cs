using ProjectMGN.DTOS.Request;
using ProjectMGN.Models;

namespace ProjectMGN.Interfaces
{
    public interface IToken
    {
        string GenerateToken(LoginRequest user);
    }
}
