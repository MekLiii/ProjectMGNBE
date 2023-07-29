﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Project> Projects { get; set; } = null!;
    }
}
