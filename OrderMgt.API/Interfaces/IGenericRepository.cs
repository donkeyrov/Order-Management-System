using System.Linq.Expressions;

namespace OrderMgt.API.Interfaces
{
    /// <summary>
    /// Geneneric interface which all interfaces inherits from to implement the default methods
    /// </summary>
    /// <typeparam name="TEntity">Class name which the interface is bound to</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        
        /// <summary>
        /// Gets an integer Id and finds a corresponding entity with the specified Id
        /// </summary>
        /// <param name="id">Id of an entity to find</param>
        /// <returns>Returns object of type [TEntity] when found or null when not found</returns>
        Task<TEntity> GetAsync(int id);
        
        /// <summary>
        /// Gets a list of all entities in collection
        /// </summary>
        /// <returns>Returns a list of all the entities in the collection</returns>
        Task<List<TEntity>> GetAllAsync();

        
        /// <summary>
        /// Gets a list of entities based on specified predicate
        /// </summary>
        /// <param name="predicate">predicate of search creteria used to select the entities</param>
        /// <returns>Returns a list of the entities selected from the collection</returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);                
        
        /// <summary>
        /// Adds an entity of type [TEntity] to the collection
        /// </summary>
        /// <param name="entity">Entity to be added to collection</param>
        Task<bool> AddAsync(TEntity entity);
        
        /// <summary>
        /// Adds multiple entities of type [TEntity] to the collection
        /// </summary>
        /// <param name="entities">List of entities to be added</param>
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates a single entity
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        Task<bool> UpdateAsync(TEntity entity);
        /// <summary>
        /// Updates multiple entities in a collection
        /// </summary>
        /// <param name="entities">List of entities to be updated</param>
        Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// Deletes a single entity from a collection
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        Task<bool> RemoveAsync(TEntity entity);
        /// <summary>
        /// Deletes multiple entities from a collection
        /// </summary>
        /// <param name="entities">List of entities to be deleted from collection</param>
        Task<bool> RemoveRangeAsync(IEnumerable<TEntity> entities);
    }
}
