using System;
using System.ComponentModel.DataAnnotations;

namespace LoginDemo.DTOs
{
    public class USUARIO
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "El usuario debe tener de 7 a 25 caracteres")]
        [RegularExpression(@"^[a-z0-9_-]{7,25}$")]
        public string Usuario { get; set; }

        [StringLength(25, MinimumLength = 10, ErrorMessage = "La contraseña debe tener de 10 a 25 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{10,25}$"
, ErrorMessage = "La contraseña debe tener mayúsculas, minúsculas, números y algún caracter especial")]
        public string Contrasena { get; set; }

        [StringLength(50, ErrorMessage = "El correo debe tener menos de 50 caracteres")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
, ErrorMessage = "El correo no comple con la estructura correspondiente")]
        public string Correo { get; set; }

        public bool Estatus { get; set; }
        public EnumSexo Sexo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}