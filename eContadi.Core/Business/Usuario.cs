using eContadi.Core.Base;
using eContadi.Views.BusinessCrud;
using eContadi.Views.BusinessList;
using System;
using Tools;



namespace eContadi.Core.Business
{
    public class Usuario : BaseAtivo
    {
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        
        public int Tipo { get; set; }
        

        //public DateTime? DataExpiracao { get; set; }



        public ViewListUsuario GetViewList()
        {
            return new ViewListUsuario()
            {
                id = id,
                Nome = Nome,
                
                Apelido = Apelido,
                Senha = Senha,
                Email = Email,
                Cargo = Cargo,
                Tipo = Tipo,
                //DataExpiracao = Coopertec.DateTimeToStr(DataExpiracao),
                Ativo = Ativo
            };
        }

        public ViewCrudUsuario GetViewCrud()
        {
            return new ViewCrudUsuario()
            {
                id = id,
                Nome = Nome,
                Apelido = Apelido,
                Cargo = Cargo,
                
                Email = Email,
                Senha = Senha,
                Ativo = Ativo,
                
                Tipo = Tipo,
                
                //DataExpiracao = Coopertec.DateTimeToStr(DataExpiracao),
                
            };
        }

        




    }

}
