using Dapper;
using Microsoft.Extensions.Configuration;
using SalesSystem.Application.Interfaces;
using SalesSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Application.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> Add(User entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            var sql = "INSERT INTO Users (Name , Surname ,Email , Password ,Role, CreatedAt , UpdatedAt ) OUTPUT INSERTED.Id VALUES (@Name , @Surname , @Email , @Password ,@Role, @CreatedAt , @UpdatedAt)";

            try
            {

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    var affectRow = connection.ExecuteScalar<int>(sql, param: entity);
                    return affectRow;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Users WHERE Id = @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var affectRow = await connection.ExecuteAsync(sql, new { Id = id });
                return affectRow;
            }
        }

        public async Task<User> Get(int id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<User>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var sql = "SELECT * FROM Users";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<User>(sql);
                return result;
            }
        }

        public async Task<int> Update(int id, User entity)
        {
            entity.UpdatedAt = DateTime.Now;

            var sql = "UPDATE Users SET Name = @Name , Surname = @Surname , Email = @Email  ,Password = @Password, Role = @Role, CreatedAt = @CreatedAt , UpdatedAt = @UpdatedAt WHERE Id = @Id";
            try
            {

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    var result = await connection.ExecuteAsync(sql, entity);
                    return result;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email";

            try
            {

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    var result = await connection.QueryAsync<User>(sql, new { Email = email });

                    return result.First();
                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
