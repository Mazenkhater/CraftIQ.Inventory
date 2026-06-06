using CraftIQ.Inventory.Core.Weappers;

namespace CraftIQ.Inventory.Core.IServicesBases;

public interface IGenericServices<TRequest, TResponse>
{
    Task<PaginatedResult<List<TResponse>>> GetAll(int pageNumber, int pageSize,string? search =null, string? orderBy = null);
    Task<TResponse> GetById(int id);
    Task<TResponse> Add(TRequest contract);
    Task Update(int id, TRequest contract);
    Task Delete(int id);
}
