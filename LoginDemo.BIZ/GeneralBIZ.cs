using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LoginDemo.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace LoginDemo.BIZ
{
    public class GeneralBIZ<Tipo> : IOperacionesRepository<Tipo> where Tipo : class
    {
        private DbContext context;

        public GeneralBIZ(DbContext context, bool lazyLoading = false)
        {            
            this.context = context;
            this.context.ChangeTracker.LazyLoadingEnabled = lazyLoading;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Tipo Insertar(Tipo entidad)
        {
            entidad = context.Set<Tipo>().Add(entidad).Entity;
            if (GuardarCambios() > 0)
                return entidad;
            else
                return null;
        }

        public Tipo Buscar(Expression<Func<Tipo, bool>> criterio)
        {
            return context.Set<Tipo>().FirstOrDefault(criterio);
        }

        public bool Actualizar(Tipo entidad)
        {
            context.Set<Tipo>().Attach(entidad);
            context.Entry(entidad).State = EntityState.Modified;
            return GuardarCambios() > 0;
        }

        public bool Eliminar(Tipo entidad)
        {
            context.Set<Tipo>().Attach(entidad);
            context.Set<Tipo>().Remove(entidad);
            return GuardarCambios() > 0;
        }

        public IEnumerable<Tipo> Listar()
        {
            return context.Set<Tipo>().ToList();
        }

        public IEnumerable<Tipo> ListarConCriterio(Expression<Func<Tipo, bool>> criterio)
        {
            return context.Set<Tipo>().Where(criterio).ToList();
        }

        public IEnumerable<Tipo> ListarIncluyendo(Expression<Func<Tipo, bool>> criterio, string incluir)
        {
            return context.Set<Tipo>().Where(criterio).Include(incluir).ToList();
        }

        public int GuardarCambios()
        {
            try
            {
                var entities = from e in context.ChangeTracker.Entries()
                               where e.State == EntityState.Added
                                   || e.State == EntityState.Modified
                               select e.Entity;
                foreach (var entity in entities)
                {
                    var validationContext = new ValidationContext(entity);
                    Validator.ValidateObject(
                        entity,
                        validationContext,
                        validateAllProperties: true);
                }

                return context.SaveChanges();
                //return context.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}
