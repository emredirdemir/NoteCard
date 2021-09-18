using Microsoft.Extensions.Configuration;
using Note.DataAccess.Abstract;
using Note.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.DataAccess.Concrete
{
    public class CategoryDal : ICategoryDal
    {
        private string _connectionString;
        public CategoryDal(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Mssql");
        }
        public async Task AddCategory(Category category)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }

                using (SqlCommand command = new SqlCommand("AddCategory", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CategoryName", category.CategoryName));

                    await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
            }
        }

        public async Task DeleteCategory(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }

                using (SqlCommand command = new SqlCommand("DeleteCategory", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", Id));
                    await command.ExecuteReaderAsync();
                }
                await connection.CloseAsync();
            }
        }

        public async Task<List<Category>> ListCategory()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                var categoryList = new List<Category>();

                using (SqlCommand command = new SqlCommand("DeleteCategory", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var response = await command.ExecuteReaderAsync())
                    {
                        while (response.Read())
                        {
                            categoryList.Add(

                                new Category
                                {
                                    Id = Convert.ToInt32(response["Id"]),
                                    CategoryName = response["CategoryName"].ToString()
                                }
                                );
                        }
                    }
                }

                return categoryList;
                await connection.CloseAsync();
            }
        }

        public async Task Update(Category category)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }

                using (SqlCommand command = new SqlCommand("UpdateCategory", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", category.Id));
                    command.Parameters.Add(new SqlParameter("@CategoryName", category.CategoryName));

                    await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
            }
        }
    }
}
