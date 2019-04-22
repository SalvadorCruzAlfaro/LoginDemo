﻿// <auto-generated />
using System;
using LoginDemo.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoginDemo.DAL.Migrations
{
    [DbContext(typeof(LoginDemoDBContext))]
    partial class LoginDemoDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LoginDemo.DTOs.USUARIO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contrasena")
                        .HasMaxLength(25);

                    b.Property<string>("Correo")
                        .HasMaxLength(50);

                    b.Property<bool>("Estatus");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<int>("Sexo");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("USUARIOs");
                });
#pragma warning restore 612, 618
        }
    }
}
