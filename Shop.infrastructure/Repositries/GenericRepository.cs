using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Shop.Core.interfaces;
using Shop.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.infrastructure.Repositries
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private  AppDbContext context { get; set; }
        public GenericRepository(AppDbContext _context) {

            context= _context;
        }
        public async Task<T> AddAsync(T entity)
        {
           var item=await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<T> DeleteAsync(int Id)
        {
            var entity = await context.Set<T>().FindAsync(Id);
            var item = context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return  item.Entity;
        }

        public  Task<IQueryable<T>> GetAllAsync()
        {
            var items= context.Set<T>().Select(a=>a);
           return Task.FromResult(items);
        }

        public  async Task<T> GetByIdAsync(int Id)
        {
            var item = await context.Set<T>().FindAsync(Id);
            return item;
        }

        public async Task<T> UpdateAsync(T entity)
        {
           var item =  context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return item.Entity;
        }
       
        public async Task<int> SaveChangesAsync()
        {
            return  await context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
           return await context.Set<T>().CountAsync();
            
        }
    }
}
