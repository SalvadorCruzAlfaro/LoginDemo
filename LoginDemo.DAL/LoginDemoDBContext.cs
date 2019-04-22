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
            optionsBuilder.UseSqlServer("Server=SU_SERVER;Database=usuariodemo;User id=SU_USUARIO;Password=SU_PASSWORD;");
        }
    }
}
