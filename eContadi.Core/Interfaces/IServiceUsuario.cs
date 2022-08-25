using System;
using System.Collections.Generic;
using System.Text;
using eContadi.Views.BusinessCrud;
using eContadi.Views.BusinessList;
//using eContadi.Views.Business;
//using eContadi.Views.Enumns;


namespace eContadi.Core.Interfaces
{
    public interface IServiceUsuario
    {
        
        ViewListRetorno<List<ViewListUsuario>> List(int tipo, int? idEmpresa, int count, int page, string filter);
        ViewCrudUsuario Get(long id);
        ViewCrudUsuario New(ViewCrudUsuario view);
        ViewCrudUsuario Update(ViewCrudUsuario view);
        string Delete(long id);
        ViewCrudUsuario Ativar(long id);
        List<ViewListUsuario> ListAtivo();
       


        





    }
}
