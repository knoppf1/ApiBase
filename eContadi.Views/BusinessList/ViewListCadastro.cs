using System;
using System.Collections.Generic;
using System.Text;

namespace eContadi.Views.BusinessList
{
    public class ViewListCadastro : _ViewListBase
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Corhexa { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
    }
}
