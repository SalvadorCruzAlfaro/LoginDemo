using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LoginDemo.Services
{
    public interface IOperacionesRepository<Tipo> : IDisposable where Tipo : class
    {
        Tipo Insertar(Tipo entidad);
        Tipo Buscar(Expression<Func<Tipo, bool>> criterio);
        bool Actualizar(Tipo entidad);
        bool Eliminar(Tipo entidad);
        IEnumerable<Tipo> Listar();
        IEnumerable<Tipo> ListarConCriterio(Expression<Func<Tipo, bool>> criterio);
        int GuardarCambios();
    }
}
