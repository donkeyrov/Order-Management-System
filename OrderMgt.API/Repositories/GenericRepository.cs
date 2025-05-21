using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrderMgt.API.Interfaces;
using OrderMgt.API.Data;
using System.Linq;

namespace OrderMgt.API.Repositories
{
    /// <summary>
    /// Generc repository that implements the iGenericRepository interface
    /// </summary>
    /// <typeparam name="TEntity">Class name which the repository is bound to</typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Default database context for the application
        /// </summary>
        public readonly AppDbContext dbContext;

        /// <summary>
        /// Contructor for the repository
        /// </summary>
        /// <param name="db">Takes in the database context through dependancy injection</param>
        public GenericRepository(AppDbContext db)
        {
             dbContext = db;
        }
        
        /// <summary>
        /// Adds an entity of type [TEntity] to the collection
        /// </summary>
        /// <param name="entity">Entity to be added to collection</param>
        public async Task<bool> AddAsync(TEntity entity)
        {
            try
            {
                await dbContext.Set<TEntity>().AddAsync(entity);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        
        /// <summary>
        /// Adds multiple entities of type [TEntity] to the collection
        /// </summary>
        /// <param name="entities">List of entities to be added</param>
        public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                await dbContext.Set<TEntity>().AddRangeAsync(entities);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a list of entities of type [TEntity] based on specified predicate
        /// </summary>
        /// <param name="predicate">predicate of search creteria used to select the entities</param>
        /// <returns>Returns a list of the entities selected from the collection</returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().Where(predicate);
        }

        
        /// <summary>
        /// Gets an integer Id and finds a corresponding entity with the specified Id
        /// </summary>
        /// <param name="id">Id of an entity to find</param>
        /// <returns>Returns object of type [TEntity] when found or null when not found</returns>
        public async Task<TEntity> GetAsync(int id)
        {
            try
            {
                var entity = await dbContext.Set<TEntity>().FindAsync(id);
                return entity;
            }
            catch
            {
                return null;
            }
        }

        
        /// <summary>
        /// Gets a list of all entities of type [TEntity] in collection
        /// </summary>
        /// <returns>Returns a list of all the entities in the collection</returns>
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await dbContext.Set<TEntity>().ToListAsync();
        }
        
        /// <summary>
        /// Deletes a single entity of type [TEntity] from a collection
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        public async Task<bool> RemoveAsync(TEntity entity)
        {
            try
            {
                dbContext.Set<TEntity>().Remove(entity);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
               

        /// <summary>
        /// Deletes multiple entities of type [TEntity] from a collection
        /// </summary>
        /// <param name="entities">List of entities to be deleted from collection</param>
        public async Task<bool> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                dbContext.Set<TEntity>().RemoveRange(entities);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        
        /// <summary>
        /// Updates a single entity of type [TEntity]
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                dbContext.Set<TEntity>().Update(entity);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        
        /// <summary>
        /// Updates multiple entities of type [TEntity] in a collection
        /// </summary>
        /// <param name="entities">List of entities to be updated</param>
        public async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                dbContext.Set<TEntity>().UpdateRange(entities);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        
    }
}
