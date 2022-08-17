using eContadi.Core.Base;
using eContadi.Data;


namespace eContadi.Services.Commons
{
    public class ServiceGenerico<TEntity> : Repository<TEntity> where TEntity : BaseEntity
    {
        public ServiceGenerico(DataContext context) : base(context)
        {
        }
    }
}