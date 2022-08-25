using eContadi.Views.BusinessList;
using System;
using System.Collections.Generic;
using System.Text;

namespace eContadi.Core.Interfaces
{
    public interface IServiceAuthentication
    {
        ViewListUsuarioAutenticado Authentication(ViewAuthentication userLogin, bool manager);
    }
}
