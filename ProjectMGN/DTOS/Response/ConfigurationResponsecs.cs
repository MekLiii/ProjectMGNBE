
using ProjectMGN.Models;
namespace ProjectMGN.DTOS.Response
{
    public class ConfigurationResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public List<ActionResponse>? Actions { get; set; } = new List<ActionResponse>();

    }
}
