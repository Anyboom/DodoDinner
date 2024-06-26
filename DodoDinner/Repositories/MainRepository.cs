﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DodoDinner.Models;
using Microsoft.EntityFrameworkCore;

namespace DodoDinner.Repositories
{
    public class MainRepository : DbContext
    {
        public DbSet<Dinner> Dinners { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;

        public MainRepository()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DodoDinner.db;");
        }
    }
}
