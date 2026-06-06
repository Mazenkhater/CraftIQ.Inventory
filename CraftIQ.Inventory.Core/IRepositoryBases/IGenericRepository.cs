using Microsoft.EntityFrameworkCore.Storage;


namespace CraftIQ.Inventory.Core.IRepositoryBases
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id); // استخدم دي لو مش عاوز اعمل انكلود او اي عملية عاوز اجيب عنصر بس 
                                      // GetTableNoTracking او GetTableAsTracking انما لو عاوز اعمل انكلود او اي عملية تانية زي فلتر او وير استخدم 
        Task<List<T>> GetAllAsync(); // استخدم دي لو عاوز تجيب كل العناصر بدون انكلود او اي عملية
                                     // GetTableNoTracking او GetTableAsTracking انما لو عاوز اعمل انكلود او اي عملية تانية زي فلتر او وير استخدم
        IQueryable<T> GetTableAsTracking(); // يعني بتتبع للتغييرات وبستخدمها للعرض والتعديل اجيب ال لستة او عنصر ثم اعدل
        IQueryable<T> GetTableNoTracking(); // يعني بدون تتبع للتغييرات وبستخدمها للعرض فقط جيت بس و ممكن اجيب لستة او عنصر
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(ICollection<T> entities);
        Task SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
    }
}
