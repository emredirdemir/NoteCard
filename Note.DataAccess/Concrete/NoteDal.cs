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
    public class NoteDal : INoteDal
    {
        private string _connectionString;
        public NoteDal(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Mssql");
        }

        public NoteCard MapToValue(SqlDataReader reader)
        {
            return new NoteCard
            {
                Id = Convert.ToInt32(reader["Id"]),
                NoteHeader = reader["NoteHeader"].ToString(),
                NoteContent = reader["NoteContent"].ToString(),
                UserId = Convert.ToInt32(reader["UserId"]),
                CategoryId = Convert.ToInt32(reader["CategoryId"])
            };

        }

        public async Task Add(NoteCard noteCard)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                using (SqlCommand command = new SqlCommand("CreateNote", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@NoteHeader", noteCard.NoteHeader));
                    command.Parameters.Add(new SqlParameter("@NoteContent", noteCard.NoteContent));
                    command.Parameters.Add(new SqlParameter("@UserId", 3));
                    command.Parameters.Add(new SqlParameter("@CategoryId", noteCard.CategoryId));

                    await command.ExecuteNonQueryAsync();
                }

                await connection.CloseAsync();
            }
        }

        public async Task Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                using (SqlCommand command = new SqlCommand("DeleteNote", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", Id));
                    await command.ExecuteNonQueryAsync();
                }
                connection.Close();
            }
        }

        public async Task<List<NoteCard>> ListNotes(int itemCount, int page)
        {
            var Notes = new List<NoteCard>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (SqlCommand command = new SqlCommand("ListNotes", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var response = await command.ExecuteReaderAsync();


                    while (await response.ReadAsync())
                    {
                        Notes.Add(MapToValue(response));
                    }
                    await connection.CloseAsync();
                    return Notes.Skip((page - 1) * itemCount).Take(itemCount).ToList();
                }
            }
        }

        public async Task<List<NoteCard>> ListByCategoryId(int Id, int itemCount, int page)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                using (SqlCommand command = new SqlCommand("ListNotesByCategoryId", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", Id));

                    var list = new List<NoteCard>();

                    using (var response = await command.ExecuteReaderAsync())
                    {
                        while (await response.ReadAsync())
                        {
                            list.Add(MapToValue(response));
                        }
                        await connection.CloseAsync();

                        return list.Skip((page - 1) * itemCount).Take(itemCount).ToList();
                    }
                }

            }
        }

        public async Task Update(NoteCard noteCard)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                using (SqlCommand command = new SqlCommand("UpdateNotes", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", noteCard.Id));
                    command.Parameters.Add(new SqlParameter("@NoteHeader", noteCard.NoteHeader));
                    command.Parameters.Add(new SqlParameter("@NoteContent", noteCard.NoteContent));
                    command.Parameters.Add(new SqlParameter("@UserId", noteCard.UserId));
                    command.Parameters.Add(new SqlParameter("@CategoryId", noteCard.CategoryId));

                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<NoteCard> GetNote(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (SqlCommand command = new SqlCommand("GetNot", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", Id));

                    var respond = command.ExecuteReader();

                    var note = new NoteCard();

                    while (respond.Read())
                    {

                        note = MapToValue(respond);

                    }
                    await connection.CloseAsync();

                    return note;

                }
            }
        }

        public int CountOfNote()
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (SqlCommand command = new SqlCommand("CountOfNote", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader["RowCount"]);
                    }

                    connection.Close();
                    return count;
                }
            }
        }

        public int GetByCountWithCategory(int Id)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (SqlCommand command = new SqlCommand("CountOfNoteWithCategory", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", Id));

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader["RowCount"]);
                    }

                    connection.Close();
                    return count;
                }
            }
        }
    }
}
