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
  public class SaleController : ControllerBase
  {
    IConfiguration configuration;
    ISaleService saleService;
    public SaleController(IConfiguration configuration, ISaleService saleService)
    {
      this.configuration = configuration;
      this.saleService = saleService;
    }

    [HttpGet]
    public ActionResult<IList<SalaDTO>> GetAll()
    {
      return saleService.GetAll().ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<SalaDTO> Get(Int64 id)
    {
      var dto = saleService.Get(id);

      if(dto != null)
        return saleService.Get(id);
      else
        return Conflict("Nie ma takiej sali");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Int64 id)
    {
      var result = saleService.Delete(id);
      if (result == null)
        return NoContent();
      else
        return BadRequest(result);
        // return Conflict("Nie ma takiej sali");
    }
  }
}