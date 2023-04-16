using System;
using System.Collections.Generic;
using System.Text;

namespace apiBase.Views.BusinessList
{
    public class ViewListUsuarioAutenticado
    {
        public long IdUsuario { get; set; }
        
        public string Nome { get; set; }
        public string Token { get; set; }
        public string msgErro { get; set; }
    }
}
