using System;
using System.Collections.Generic;


namespace apiBase.Views.BusinessList
{
    public class ViewListRetorno<T>
    {
        public int Total { get; set; }
        public T Dados { get; set; }


    }
}
