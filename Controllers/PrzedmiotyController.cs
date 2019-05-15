using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PlanLekcji.DTO;
using PlanLekcji.Services;

namespace PlanLekcji.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PrzedmiotyController : ControllerBase
  {
    IConfiguration configuration;
    IPrzedmiotyService przedmiotyService;
    public PrzedmiotyController(IConfiguration configuration, IPrzedmiotyService przedmiotyService)
    {
      this.configuration = configuration;
      this.przedmiotyService = przedmiotyService;
    }

    [HttpGet]
    public ActionResult<IList<PrzedmiotDTO>> GetAll()
    {
      return przedmiotyService.GetAll().ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<PrzedmiotDTO> Get(Int64 id)
    {
      return przedmiotyService.Get(id);
    }

    [HttpPost]
    public IActionResult Create([FromBody]PrzedmiotAddDTO dto)
    {
      if (dto == null || string.IsNullOrWhiteSpace(dto.Nazwa))
        return BadRequest();
      var result = przedmiotyService.Create(dto);
      if (result == true)
        return NoContent();
      else
        return BadRequest();      
    }
  }
}