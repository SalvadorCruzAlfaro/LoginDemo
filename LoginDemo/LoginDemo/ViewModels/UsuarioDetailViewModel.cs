using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoginDemo.DTOs;

namespace LoginDemo.ViewModels
{
    public class UsuarioDetailViewModel : BaseViewModel
    {
        public List<EnumSexo> ListaSexo { get; set; }
        public USUARIO Item { get; set; }
        public UsuarioDetailViewModel(USUARIO item = null)
        {
            Title = item?.Usuario;
            Item = item;
            ListaSexo = Enum.GetValues(typeof(EnumSexo)).Cast<EnumSexo>().ToList();
        }
    }
}
