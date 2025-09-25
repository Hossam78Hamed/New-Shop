using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.interfaces
{
    public interface IGenericRepository<T> where T: class
    {
        public Task<IQueryable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int Id);
        public  Task<T> AddAsync(T entity );
        public Task<T> UpdateAsync(T entity);
        public Task<T> DeleteAsync(int Id);
        public Task<int> CountAsync();
        public Task<int> SaveChangesAsync();

       
    }
}
/*
        Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] Includes);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
       
 */
