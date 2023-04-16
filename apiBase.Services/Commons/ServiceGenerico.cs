using apiBase.Core.Base;
using apiBase.Data;


namespace apiBase.Services.Commons
{
    public class ServiceGenerico<TEntity> : Repository<TEntity> where TEntity : BaseEntity
    {
        public ServiceGenerico(DataContext context) : base(context)
        {
        }
    }
}