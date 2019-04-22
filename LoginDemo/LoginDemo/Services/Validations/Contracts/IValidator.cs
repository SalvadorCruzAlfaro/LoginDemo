using System;
using System.Collections.Generic;
using System.Text;

namespace LoginDemo.Services.Validations.Contracts
{
    public interface IValidator
    {
        string Message { get; set; }
        bool Check(string value);
    }
}
