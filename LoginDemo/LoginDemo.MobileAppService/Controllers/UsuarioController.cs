using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LoginDemo.DTOs;
using LoginDemo.DTOs.Partials;
using LoginDemo.MobileAppService.Generics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginDemo.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ApiGeneral<USUARIO>
    {
        string key = "E546C8DF278CD5931069B522E695D4F2";

        [HttpPost("InsertarElemento")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Insertar([FromBody] USUARIO entidad)
        {
            try
            {
                var encontrado = context.Buscar(x => x.Usuario.Equals(entidad.Usuario));
                if (encontrado != null)
                    return BadRequest("Usuario ya existe");
                entidad.FechaCreacion = DateTime.Now;
                entidad.Id = new Guid();
                entidad.Estatus = true;
                var respuesta = context.Insertar(entidad);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("EliminarElemento")]
        public IActionResult Eliminar([FromBody]USUARIO entidad)
        {
            try
            {
                entidad.Estatus = false;
                return Ok(context.Actualizar(entidad));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("ValidarLogin")]
        public IActionResult Validar([FromBody]USUARIOVALIDAR entidad)
        {
            try
            {
                string contrasena = DesEncriptar(entidad.Contraseña);
                string usuario = DesEncriptar(entidad.Usuario);
                var respuesta = context.Buscar(x => x.Contrasena.Equals(contrasena) && x.Usuario.Equals(usuario) && x.Estatus);
                if (respuesta != null)
                    return Ok(respuesta);
                else
                    return BadRequest("No coincide el usuario y/o password proporcionados");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private string Encriptar(string cadena)
        {
            var key = Encoding.UTF8.GetBytes(this.key);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(cadena);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }

        }

        private string DesEncriptar(string cadena)
        {

            var fullCipher = Convert.FromBase64String(cadena);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(this.key);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }

        }
    }
}