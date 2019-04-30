using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LoginDemo.DTOs;

namespace LoginDemo.DAL
{
    public class LoginDemoDBContext : DbContext
    {
        public DbSet<USUARIO> USUARIOs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:pruebasca.database.windows.net,1433;Database=usuariodemo;User Id=scruz;Password=2019Pru3b@;");
        }
    }
}
