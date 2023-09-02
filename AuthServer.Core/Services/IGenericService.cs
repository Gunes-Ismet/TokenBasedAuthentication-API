using SharedLibrary.DTO_s;
using System.Linq.Expressions;

namespace AuthServer.Core.Services
{
    public interface IGenericService<TEntity, Dto> where TEntity : class where Dto : class
    {
        Task<Response<Dto>> GetByIdAsync(int id);
        Task<Response<IEnumerable<Dto>>> GetAllAsync();
        Task<Response<IEnumerable<Dto>>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<Response<Dto>> AddAsync(Dto dto);
        Task<Response<NoDataDTO>> Remove(int id);
        Task<Response<NoDataDTO>> Update(Dto dto, int id);
    }
}
