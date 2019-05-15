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

        [HttpDelete("{id}")]
        public ActionResult Delete(Int64 id)
        {
            if(saleService.Delete(id)) {
                return NoContent();
            } else {
                return BadRequest();
            }
            
        }
    }
}
