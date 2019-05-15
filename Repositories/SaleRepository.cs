using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlanLekcji.DTO;
using System;

namespace PlanLekcji.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        IConfiguration configuration;
        public SaleRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
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

        public bool Delete(Int64 id)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand command = new SqlCommand(@"SELECT COUNT(SalaId) FROM PozycjaPlanuLekcji WHERE SalaId = @Id", connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add("Id", SqlDbType.BigInt);
                command.Parameters["Id"].Value = id;
                connection.Open();
                var reader = command.ExecuteScalar();
                if ((Int32)reader > 0)
                {
                    connection.Close();
                    return false;
                }
                else
                {
                    SqlCommand delCommand = new SqlCommand("DELETE FROM SALE WHERE ID = @ID", connection);
                    delCommand.CommandType = System.Data.CommandType.Text;
                    delCommand.Parameters.Add("Id", SqlDbType.BigInt);
                    delCommand.Parameters["Id"].Value = id;
                    connection.Open();
                    var result = command.ExecuteNonQuery();

                    connection.Close();
                    return true;
                }
            }
        }
    }
}