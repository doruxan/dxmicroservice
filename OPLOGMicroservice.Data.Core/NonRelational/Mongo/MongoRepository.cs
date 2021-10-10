using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Data.Core.NonRelational.Mongo
{
    class MongoRepository
    {
    }
    public class MongoRepository<T> : IRepository<T> where T : MongoEntity
    {
        private readonly IMongoContext context;
        private IMongoCollection<T> dbSet;

        public MongoRepository(IMongoContext context)
        {
            this.context = context;

            dbSet = context.GetCollection<T>(typeof(T).Name);
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedDate = DateTimeOffset.UtcNow;
            entity.UnixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            context.AddCommand(() => dbSet.InsertOneAsync(entity));
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var data = await dbSet.FindAsync(x => x.Id == id.ToString());
            return data.SingleOrDefault();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var all = await dbSet.FindAsync(Builders<T>.Filter.Empty);
            return all.ToList();
        }

        public async Task UpdateAsync(T entity)
        {
            entity.ModifiedDate = DateTimeOffset.UtcNow;
            context.AddCommand(() => dbSet.ReplaceOneAsync(x => x.Id == entity.Id.ToString(), entity));
        }

        public async Task DeleteAsync(T entity)
        {
            context.AddCommand(() => dbSet.DeleteOneAsync(x => x.Id == entity.Id.ToString()));
        }

        public IQueryable<T> GetQueryable()
        {
            return dbSet.AsQueryable();
        }

        public async Task DeleteAsync(object id)
        {
            context.AddCommand(() => dbSet.DeleteOneAsync(x => x.Id == id.ToString()));
        }
    }
}
