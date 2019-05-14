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
  public class KlasyController : ControllerBase
  {
    IConfiguration configuration;
    public KlasyController(IConfiguration configuration)
    {
      this.configuration = configuration;
    }

    [HttpGet]
    public ActionResult<IList<KlasaDTO>> GetAll()
    {
      List<KlasaDTO> list = new List<KlasaDTO>();
      using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
      {

        SqlCommand command = new SqlCommand(@"SELECT K.Id, K.Nazwa, K.Rocznik, N.Imie, N.Nazwisko FROM Klasy K join Nauczyciele N on K.WychowawcaId = N.Id", connection);
        command.CommandType = System.Data.CommandType.Text;
        connection.Open();
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
          KlasaDTO dto = new KlasaDTO();
          dto.Id = reader.GetInt64(0);
          dto.Nazwa = reader.GetString(1);
          dto.Rocznik = reader.GetInt32(2);
          dto.Imie = reader.GetString(3);
          dto.Nazwisko = reader.GetString(4);
          list.Add(dto);
        }
        connection.Close();
      }
      return list;
    }

    [HttpGet("{id}")]
    public ActionResult<KlasaDTO> Get(Int64 id)
    {
      KlasaDTO dto = new KlasaDTO();
      using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
      {

        SqlCommand command = new SqlCommand(@"SELECT K.Id, K.Nazwa, K.Rocznik, N.Imie, N.Nazwisko FROM Klasy K join Nauczyciele N on K.WychowawcaId = N.Id WHERE K.Id = @Id", connection);
        command.CommandType = System.Data.CommandType.Text;
        command.Parameters.Add("Id", SqlDbType.BigInt);
        command.Parameters["Id"].Value = id;
        connection.Open();
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
          dto.Id = reader.GetInt64(0);
          dto.Nazwa = reader.GetString(1);
          dto.Rocznik = reader.GetInt32(2);
          dto.Imie = reader.GetString(3);
          dto.Nazwisko = reader.GetString(4);
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

    [HttpPost]
    public IActionResult Create([FromBody]KlasaAddDTO dto)
    {
      if (dto == null || string.IsNullOrWhiteSpace(dto.Nazwa))
        return BadRequest();
      using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
      {
        try
        {
          SqlCommand command = new SqlCommand(@"INSERT INTO Klasy (NAZWA, ROCZNIK, WYCHOWAWCAID) VALUES (@nazwa, @rocznik, @wychowawcaId)", connection);
          command.CommandType = System.Data.CommandType.Text;
          command.Parameters.Add("nazwa", SqlDbType.VarChar);
          command.Parameters["nazwa"].Value = dto.Nazwa;
          command.Parameters.Add("rocznik", SqlDbType.Int);
          command.Parameters["rocznik"].Value = dto.Rocznik;
          command.Parameters.Add("wychowawcaId", SqlDbType.BigInt);
          command.Parameters["wychowawcaId"].Value = dto.WychowawcaId;
          connection.Open();
          var result = command.ExecuteNonQuery();
        }
        catch
        {
          return BadRequest();
        }
        finally
        {
          connection.Close();
        }
      }
      return NoContent();
    }
  }
}