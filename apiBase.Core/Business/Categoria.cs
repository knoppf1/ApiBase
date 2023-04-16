using apiBase.Core.Base;
using apiBase.Views.BusinessCrud;
using apiBase.Views.BusinessList;



namespace apiBase.Core.Business
{
    //Espelho do Categoria de Dados (Campos e Tipagem)
    public class Categoria : BaseAtivo
    {
        public string Nome { get; set; }
        
        //Chaves estrangeiras
        

        public ViewListCategoria GetViewList()
        {
            return new ViewListCategoria()
            {
                //ViewList = (BaseEntity/BaseAtivo)/This
                id = id,
                Nome = Nome,
                
                Ativo = Ativo
            };
        }
        public ViewCrudCategoria GetViewCrud()
        {
            return new ViewCrudCategoria()
            {
                //ViewCrud = (BaseEntity/BaseAtivo)/This
                id = id,
                Nome = Nome,
                
                Ativo = Ativo,
                
            };
        }
    }


}
