using System;
using System.Collections.Generic;
using System.Text;

namespace eContadi.Views.BusinessList
{
    public class ViewListUsuario : _ViewListBase
    {
        public string Nome { get; set; }
        
        public string Apelido { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        
        public int Tipo { get; set; }
        
        //public string? DataExpiracao { get; set; }

    }
}
