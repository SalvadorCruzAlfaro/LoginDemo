using LoginDemo.BIZ;
using LoginDemo.DAL;
using LoginDemo.DTOs;
using System;

namespace LoginDemo.GenerateDB
{
    class Program
    {

        /// <summary>
        /// TODO: Para crear DB a partir de CodeFirst
        /// Abrir consola de nuget
        /// Poner como proyecto de inicio el proyecto de consola
        /// Seleccionar en la consola de nuget el defatul project que contenga la DAL
        /// Ejecutar este comando en la consola de nuget: Add-Migration InitialCreate
        /// Ejecutar el comando Update-Database para actualizar la base de datos
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                var contexto = new GeneralBIZ<USUARIO>(new LoginDemoDBContext(), false);
                contexto.Insertar(new USUARIO
                {
                    Usuario = "isc_scruz",
                    Contrasena = "uNagrande33!",
                    Correo = "isc_scruz@hotmail.com",
                    Sexo = EnumSexo.Masculino,
                    Estatus = true,
                    FechaCreacion = DateTime.Now,
                    Id = new Guid()
                });
                var lista = contexto.Listar();
                foreach (USUARIO elemento in lista)
                {
                    Console.WriteLine($"Clave: {elemento.Id}\nUsuario: {elemento.Usuario}\nContraseña: {elemento.Contrasena}\nSexo: {elemento.Sexo.ToString()}\nFechaCreacion: {elemento.FechaCreacion.ToLongDateString()}\n\n");
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
