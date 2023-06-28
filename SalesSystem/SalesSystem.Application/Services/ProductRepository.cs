using Dapper;
using SalesSystem.Application.Context;
using SalesSystem.Application.Interfaces;
using SalesSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Application.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlServerConnection _sqlServerConnection;

        public ProductRepository(SqlServerConnection sqlServerConnection)
        {
            _sqlServerConnection = sqlServerConnection;
        }

        public async Task<int> Add(Product entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            var sqlCommand = "INSERT INTO Products (Name,Price , Description,IsDeleted,CreatedAt,UpdatedAt) OUTPUT INSERTED.Id VALUES (@Name,@Price , @Description,@IsDeleted,@CreatedAt,@UpdatedAt) ";
            try
            {
                using (IDbConnection dataBase = _sqlServerConnection.Connect())
                {
                    dataBase.Open();
                    var affectedRow = dataBase.ExecuteScalar<int>(sqlCommand, param: entity);
                    return affectedRow;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sqlCommand = "UPDATE Products SET IsDeleted = 1 WHERE Id = @Id AND IsDeleted = 0";
            try
            {
                using (IDbConnection dataBase = _sqlServerConnection.Connect())
                {
                    dataBase.Open();
                    var affectRow = await dataBase.ExecuteAsync(sqlCommand, new { Id = id });
                    return affectRow;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<Product> Get(int id)
        {
            try
            {
                var sql = "SELECT * FROM Products WHERE Id = @Id AND IsDeleted = 0";
                using (IDbConnection dataBase = _sqlServerConnection.Connect())
                {
                    dataBase.Open();
                    var result = await dataBase.QueryAsync<Product>(sql, new { Id = id });
                    return result.First();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {

            try
            {
                var sql = "SELECT * FROM Products WHERE IsDeleted = 0";

                using (IDbConnection dataBase = _sqlServerConnection.Connect())
                {
                    dataBase.Open();
                    var result = await dataBase.QueryAsync<Product>(sql);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Update(int id, Product entity)
        {
            entity.UpdatedAt = DateTime.Now;

            var sql = "UPDATE Products SET Name = @Name , Price = @Price , Description = @Description, IsDeleted = @IsDeleted  ,CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt WHERE Id = @Id AND IsDeleted = 0";
            try
            {

                using (IDbConnection dataBase = _sqlServerConnection.Connect())
                {
                    dataBase.Open();
                    var result = await dataBase.ExecuteAsync(sql, entity);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

       


    }
}
