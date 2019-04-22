using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LoginDemo.BIZ;
using LoginDemo.DAL;
using LoginDemo.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginDemo.MobileAppService.Generics
{
    public class ApiGeneral<Tipo> : ControllerBase where Tipo : class, new()
    {
        public GeneralBIZ<Tipo> context;

        public ApiGeneral()
        {
            context = new GeneralBIZ<Tipo>(new LoginDemoDBContext());
        }


        //[HttpPost("InsertarElemento")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult Insertar([FromBody] Tipo entidad)
        //{
        //    try
        //    {
        //        var respuesta = context.Insertar(entidad);
        //        return Ok(respuesta);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Buscar(Guid id)
        {
            try
            {
                var parametro = Expression.Parameter(Type.GetType(nameof(Tipo), true), "x");
                var cuerpo = Expression.Equal(Expression.PropertyOrField(parametro, "Id"), Expression.Constant(id));
                var lambda = Expression.Lambda<Func<Tipo, bool>>(cuerpo, parametro);
                var respuesta = context.Buscar(lambda);
                if (respuesta == null)
                    return NotFound();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("ActualizarElemento")]
        public IActionResult Actualizar([FromBody]Tipo entidad)
        {
            try
            {
                return Ok(context.Actualizar(entidad));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ListaElementos")]
        public IActionResult Listar()
        {
            try
            {
                return Ok(context.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}