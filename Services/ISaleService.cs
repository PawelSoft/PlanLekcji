using System;
using System.Collections.Generic;
using PlanLekcji.DTO;

namespace PlanLekcji.Services
{
    public interface ISaleService
    {
       IList<SalaDTO> GetAll();
       SalaDTO Get(Int64 id);

       string Delete(Int64 id);
    }
}