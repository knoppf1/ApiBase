using System;
using System.Collections.Generic;
using System.Text;
using eContadi.Views.BusinessCrud;
using eContadi.Views.BusinessList;
//using eContadi.Views.Enumns;


namespace eContadi.Core.Interfaces
{
    //Funçoes que o controler pode executar (Interligação com a API)
    public interface IServiceCategoria
    {
        ViewListRetorno<List<ViewListCategoria>> List(int idEmpresa, int count, int page, string filter);
        ViewCrudCategoria Get(long id);
        ViewCrudCategoria New(ViewCrudCategoria view);
        ViewCrudCategoria Update(ViewCrudCategoria view);
        string Delete(long id);
        ViewCrudCategoria Ativar(long id);
        List<ViewListCategoria> ListAtivo(int idEmpresa);
    }
}
