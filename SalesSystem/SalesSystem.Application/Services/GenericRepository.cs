using Dapper;
using SalesSystem.Application.Interfaces;
using SalesSytem.Data.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Application.Services
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected IDbConnection DbConnection { get; set; }

        private readonly DatabaseSettings _databaseSettings;

        public GenericRepository(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;

            //DbConnection = new DbContext().GetDbContext(_databaseSettings.ConnectionString);
        }

        public async Task<int> Add(TEntity entity)
        {
            throw new NotImplementedException();

        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(int id, TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
