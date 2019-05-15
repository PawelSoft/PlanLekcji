using System;
using System.Collections.Generic;
using PlanLekcji.DTO;

namespace PlanLekcji.Repositories
{
    public interface ISaleRepository
    {
        IList<SalaDTO> GetAll();

        bool Delete(Int64 id);
    }
}