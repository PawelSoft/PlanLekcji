using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlanLekcji.DTO;

namespace PlanLekcji.Repositories
{
    public class PrzedmiotyRepository : IPrzedmiotyRepository
    {
        IConfiguration configuration;
        public PrzedmiotyRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool Create(PrzedmiotAddDTO dto)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                SqlCommand command = new SqlCommand(@"INSERT INTO Przedmioty (NAZWA) VALUES (@nazwa)", connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add("nazwa", SqlDbType.VarChar);
                command.Parameters["nazwa"].Value = dto.Nazwa;
                connection.Open();
                var result = command.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return true;
        }

        public PrzedmiotDTO Get(long id)
        {
            PrzedmiotDTO dto = new PrzedmiotDTO();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {

                SqlCommand command = new SqlCommand(@"SELECT ID, NAZWA FROM PRZEDMIOTY 
                            WHERE Id = @Id", connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add("Id", SqlDbType.BigInt);
                command.Parameters["Id"].Value = id;
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                dto.Id = reader.GetInt64(0);
                dto.Nazwa = reader.GetString(1);
                connection.Close();
                }
                else
                {
                connection.Close();
                return null;
                }
            }
            return dto;
        }

        public IList<PrzedmiotDTO> GetAll()
        {
            List<PrzedmiotDTO> list = new List<PrzedmiotDTO>();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {

                SqlCommand command = new SqlCommand("SELECT ID, NAZWA FROM PRZEDMIOTY", connection);
                command.CommandType = System.Data.CommandType.Text;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PrzedmiotDTO dto = new PrzedmiotDTO();
                    dto.Id = reader.GetInt64(0);
                    dto.Nazwa = reader.IsDBNull(1) ? null : reader.GetString(1);
                    list.Add(dto);
                }
                connection.Close();
            }
            return list;
        }

        public bool SprawdzCzyNazwaIstnieje(string nazwa)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {

                SqlCommand command = new SqlCommand("SELECT ID FROM PRZEDMIOTY WHERE NAZWA = @nazwa", connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@nazwa", nazwa);
                connection.Open();
                var result = command.ExecuteScalar();

                connection.Close();
                if (result == null)
                    return false;
                else
                    return true;
                //return result == null ? false : true;
            }
        }
    }
}