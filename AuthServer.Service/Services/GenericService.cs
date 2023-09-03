using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using AuthServer.Service.AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO_s;
using System.Linq.Expressions;

namespace AuthServer.Service.Services
{
    public class GenericService<TEntity, Dto> : IGenericService<TEntity, Dto> where TEntity : class where Dto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _repository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Response<Dto>> AddAsync(Dto dto)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(dto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = ObjectMapper.Mapper.Map<Dto>(newEntity);
            return Response<Dto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<Dto>>> GetAllAsync()
        {
            var dtos = ObjectMapper.Mapper.Map<List<Dto>>(await _repository.GetAllAsync());
            return Response<IEnumerable<Dto>>.Success(dtos, 200);
        }

        public async Task<Response<Dto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return Response<Dto>.Fail("Id not Found!", 404, true);
            }            
            return Response<Dto>.Success(ObjectMapper.Mapper.Map<Dto>(entity), 200);
        }

        public async Task<Response<NoDataDTO>> RemoveAsync(int id)
        {
            var isExistEntity = await _repository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDTO>.Fail("Id not Found!",404,true);
            }
            _repository.Remove(isExistEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<NoDataDTO>> UpdateAsync(Dto dto,int id)
        {
            var isExistEntity = await _repository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDTO>.Fail("Id not Found!", 404, true);
            }
            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(dto);
            _repository.Update(updateEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<IEnumerable<Dto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _repository.Where(predicate);
            return Response<IEnumerable<Dto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<Dto>>(await list.ToListAsync()),200);
        }
    }
}
