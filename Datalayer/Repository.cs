using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Datalayer
{
    public class Repository<T>
        where T : class
    {
        protected AppDbContext context;
        protected DbSet<T> dbSet;

        public Repository(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        /// <summary>
        ///  Add a new entity to the Table.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }
        /// <summary>
        ///  Find a set of entities that match a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return dbSet.Where(predicate);
        }
        /// <summary>
        ///  Find the first entity that match a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return dbSet.FirstOrDefault(predicate)!;
        }
        public IEnumerable<T> Find(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = dbSet.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return query.Where(predicate);
        }
        public T FirstOrDefault(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = dbSet.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return query.FirstOrDefault(predicate)!;
        }
    }
}
