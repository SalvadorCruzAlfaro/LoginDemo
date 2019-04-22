using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using LoginDemo.DTOs;
using LoginDemo.DTOs.Partials;
using LoginDemo.Views;
using Xamarin.Forms;

namespace LoginDemo.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        string key = "E546C8DF278CD5931069B522E695D4F2";

        public LoginViewModel()
        {            
            USUARIOVALIDAR validar = null;
            MessagingCenter.Subscribe<LoginPage, USUARIO>(this, "LoginUsuario", async (obj, item) =>
            {
                var newItem = item as USUARIO;
                //Items.Add(newItem);
                //if (item.Id != null)
                //    Logueado = true;
                validar=new USUARIOVALIDAR
                {
                    Usuario = Encriptar(newItem.Usuario),
                    Contraseña = Encriptar(newItem.Contrasena)
                };
                bool respuesta = await DataStore.LoginItemAsync(validar);
                if (respuesta)
                {
                    Logueado = true;
                    SinSesion = false;
                    MessagingCenter.Send(this, "Logueado", true);
                }
                else
                {
                    SinSesion = true;
                    MessagingCenter.Send(this, "Logueado", false);
                }
            });
            Logueado = false;
            SinSesion = true;
        }

        private bool logueado;
        public bool Logueado
        {
            get { return logueado; }
            set { SetProperty(ref logueado, value); }
        }

        private bool sinSesion;

        public bool SinSesion
        {
            get { return sinSesion; }
            set { SetProperty(ref sinSesion, value); }
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
    }
}
