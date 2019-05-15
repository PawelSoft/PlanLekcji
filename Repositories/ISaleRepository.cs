using System;
using System.Collections.Generic;
using PlanLekcji.DTO;

namespace PlanLekcji.Repositories
{
    public interface ISaleRepository
    {
        IList<SalaDTO> GetAll();
       SalaDTO Get(Int64 id);
       bool Delete(Int64 id);

       bool SprawdzCzyIdIstnieje(Int64 id);
    }
}