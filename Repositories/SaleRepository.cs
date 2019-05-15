using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlanLekcji.DTO;

namespace PlanLekcji.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        IConfiguration configuration;
        public SaleRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

         public bool Delete(Int64 id)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                SqlCommand command = new SqlCommand(@"DELETE FROM Sale WHERE id = @id", connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add("id", SqlDbType.BigInt);
                command.Parameters["id"].Value = id;
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

        public SalaDTO Get(long id)
        {
            SalaDTO dto = new SalaDTO();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {

                SqlCommand command = new SqlCommand(@"SELECT ID, NAZWA FROM SALE WHERE Id = @Id", connection);
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

        public IList<SalaDTO> GetAll()
        {
            List<SalaDTO> list = new List<SalaDTO>();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {

                SqlCommand command = new SqlCommand("SELECT ID, NAZWA FROM SALE", connection);
                command.CommandType = System.Data.CommandType.Text;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SalaDTO dto = new SalaDTO();
                    dto.Id = reader.GetInt64(0);
                    dto.Nazwa = reader.IsDBNull(1) ? null : reader.GetString(1);
                    list.Add(dto);
                }
                connection.Close();
            }
            return list;
        }

        public bool SprawdzCzyIdIstnieje(Int64 id)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {

                SqlCommand command = new SqlCommand("SELECT ID FROM SALE WHERE id = @Id", connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var result = command.ExecuteScalar();

                connection.Close();
                if (result == null)
                    return false;
                else
                    return true;
            }
        }
    }
}