using eContadi.Core.Base;
using eContadi.Views.BusinessCrud;
using eContadi.Views.BusinessList;



namespace eContadi.Core.Business
{
    //Espelho do Categoria de Dados (Campos e Tipagem)
    public class Cadastro : BaseAtivo
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





        public ViewListCadastro GetViewList()
        {
            return new ViewListCadastro()
            {
                //ViewList = (BaseEntity/BaseAtivo)/This
                id = id,
                Nome = Nome,
                Cpf = Cpf,
                DataNascimento = DataNascimento,
                Email = Email,
                Telefone = Telefone,
                Endereco = Endereco,
                Corhexa = Corhexa,
                Lat = Lat,
                Lng = Lng,
                Ativo = Ativo,
            };
        }
        public ViewCrudCadastro GetViewCrud()
        {
            return new ViewCrudCadastro()
            {
                //ViewCrud = (BaseEntity/BaseAtivo)/This
                id = id,
                Nome = Nome,
                Cpf = Cpf,
                DataNascimento = DataNascimento,
                Email = Email,
                Telefone = Telefone,
                Endereco = Endereco,
                Corhexa = Corhexa,
                Lat = Lat,
                Lng = Lng,
                Ativo = Ativo,
            };
        }
    }


}
