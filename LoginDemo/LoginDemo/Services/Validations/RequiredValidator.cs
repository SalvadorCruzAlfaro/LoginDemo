using System;
using System.Collections.Generic;
using System.Text;
using LoginDemo.Services.Validations.Contracts;

namespace LoginDemo.Services.Validations
{
    public class RequiredValidator : IValidator
    {
        public string Message { get; set; } = "El campo es requerido";

        public bool Check(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
