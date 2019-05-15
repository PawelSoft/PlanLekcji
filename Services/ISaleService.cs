using System;
using System.Collections.Generic;
using PlanLekcji.DTO;

namespace PlanLekcji.Services
{
    public interface ISaleService
    {
        IList<SalaDTO> GetAll();
        bool Delete(Int64 id);
    }
}