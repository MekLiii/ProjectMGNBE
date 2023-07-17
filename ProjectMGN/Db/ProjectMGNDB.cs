using Microsoft.EntityFrameworkCore;
using ProjectMGN.Models;

namespace ProjectMGN.Db
{
    public class ProjectMGNDB : DbContext
    {
        public ProjectMGNDB(DbContextOptions<ProjectMGNDB> options) : base(options)
        {
            Console.WriteLine(options);
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Projects> Projects { get; set; } = null!;


        public void TestConnection()
        {
            try
            {
                using (var connection = Database.GetDbConnection())
                {
                    connection.Open();
                    Console.WriteLine("Succes");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
