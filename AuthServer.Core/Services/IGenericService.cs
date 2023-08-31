using SharedLibrary.DTO_s;
using System.Linq.Expressions;

namespace AuthServer.Core.Services
{
    public interface IGenericService<TEntity, TDTO> where TEntity : class where TDTO : class
    {
        Task<Response<TDTO>> GetByIdAsync(int id);
        Task<Response<IEnumerable<TDTO>>> GetAllAsync();
        Task<Response<IEnumerable<TDTO>>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<Response<TDTO>> AddAsync(TEntity entity);
        Task<Response<NoDataDTO>> Remove(TEntity entity);
        Task<Response<NoDataDTO>> Update(TEntity entity);
    }
}
