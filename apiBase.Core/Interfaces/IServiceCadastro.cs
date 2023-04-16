using System;
using System.Collections.Generic;
using System.Text;
using apiBase.Views.BusinessCrud;
using apiBase.Views.BusinessList;
//using apiBase.Views.Enumns;


namespace apiBase.Core.Interfaces
{
    //Funçoes que o controler pode executar (Interligação com a API)
    public interface IServiceCadastro
    {
        ViewListRetorno<List<ViewListCadastro>> List(int idEmpresa, int count, int page, string filter);
        ViewCrudCadastro Get(long id);
        ViewCrudCadastro New(ViewCrudCadastro view);
        ViewCrudCadastro Update(ViewCrudCadastro view);
        string Delete(long id);
        ViewCrudCadastro Ativar(long id);
        List<ViewListCadastro> ListAtivo(int idEmpresa);
    }
}
