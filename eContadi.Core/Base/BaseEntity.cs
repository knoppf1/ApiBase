using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace eContadi.Core.Base
{
   public class BaseEntity : DbContext
    {
        public long id { get; set; }
        public bool Excluido { get; set; }
       
    }
}
