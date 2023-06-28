using Dapper;
using Microsoft.Extensions.Configuration;
using SalesSystem.Application.Context;
using SalesSystem.Application.Interfaces;
using SalesSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Application.Services
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        private readonly SqlServerConnection _sqlServerConnection;

        public DiscountRepository(IConfiguration configuration, SqlServerConnection sqlServerConnection)
        {
            _configuration = configuration;
            _sqlServerConnection = sqlServerConnection;
        }

        public async Task<int> Add(Discount entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            var sqlCommand = "INSERT INTO Discounts (Title,Percentage , IsDeleted,CreatedAt,UpdatedAt,ExpireDate) OUTPUT INSERTED.Id VALUES (@Title , @Percentage,@IsDeleted,@CreatedAt,@UpdatedAt,@ExpireDate) ";
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
            var sqlCommand = "UPDATE Discounts SET IsDeleted = 1 WHERE Id = @Id";
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

        public async Task<Discount> Get(int id)
        {
            try
            {
                var sql = "SELECT * FROM Discounts WHERE Id = @Id AND IsDeleted = 0";
                using (IDbConnection dataBase = _sqlServerConnection.Connect())
                {
                    dataBase.Open();
                    var result = await dataBase.QueryAsync<Discount>(sql, new { Id = id });
                    return result.First();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IEnumerable<Discount>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Discounts WHERE IsDeleted = 0";

                using (IDbConnection dataBase = _sqlServerConnection.Connect())
                {
                    dataBase.Open();
                    var result = await dataBase.QueryAsync<Discount>(sql);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Update(int id, Discount entity)
        {
            entity.UpdatedAt = DateTime.Now;

            var sql = "UPDATE Discounts SET Title = @Title , Percentage = @Percentage , IsDeleted = @IsDeleted  ,CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt, ExpireDate = @ExpireDate WHERE Id = @Id AND IsDeleted = 0";
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
