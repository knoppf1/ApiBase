using apiBase.Views.BusinessList;
using System;
using System.Collections.Generic;
using System.Text;

namespace apiBase.Core.Interfaces
{
    public interface IServiceAuthentication
    {
        ViewListUsuarioAutenticado Authentication(ViewAuthentication userLogin, bool manager);
    }
}
