using System;
using System.Collections.Generic;
using System.Text;
using apiBase.Views.BusinessCrud;
using apiBase.Views.BusinessList;
//using apiBase.Views.Business;
//using apiBase.Views.Enumns;


namespace apiBase.Core.Interfaces
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
