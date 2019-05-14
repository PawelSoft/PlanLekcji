using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PlanLekcji.DTO;

namespace PlanLekcji.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrzedmiotyController : ControllerBase
    {
        IConfiguration configuration;
        public PrzedmiotyController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IList<PrzedmiotyDTO>> GetAll()
        {
            List<PrzedmiotyDTO> list = new List<PrzedmiotyDTO>();
             using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {

                SqlCommand command = new SqlCommand("SELECT ID, NAZWA FROM PRZEDMIOTY", connection);
                command.CommandType = System.Data.CommandType.Text;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PrzedmiotyDTO dto = new PrzedmiotyDTO();
                    dto.Id = reader.GetInt64(0);
                    dto.Nazwa = reader.GetString(1);
                    list.Add(dto);
                }
                connection.Close();
            }
            return list;
        }

        [HttpGet("{id}")]
        public ActionResult<PrzedmiotyDTO> Get(Int64 id)
        {
            PrzedmiotyDTO dto = new PrzedmiotyDTO();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {

                SqlCommand command = new SqlCommand("SELECT ID, NAZWA FROM PRZEDMIOTY WHERE Id = @Id", connection);
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
                    return NotFound();
                }
            }
            return dto;
        }
    }
}