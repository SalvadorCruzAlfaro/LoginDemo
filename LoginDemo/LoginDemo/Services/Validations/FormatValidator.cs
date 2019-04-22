using LoginDemo.Services.Validations.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LoginDemo.Services.Validations
{
    // Correo
    // Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    // Password
    // Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{10,25}$");
    public class FormatValidator : IValidator
    {
        public string Message { get; set; } = "Formato invalido";
        public string Format { get; set; }

        public bool Check(string value)
        {
            if(!string.IsNullOrEmpty(value))
            {
                Regex format = new Regex(Format);
                return format.IsMatch(value);
            }
            return false;
        }
    }
}
