using System;
using System.Collections.Generic;
using PlanLekcji.DTO;

namespace PlanLekcji.Services
{
    public interface IPrzedmiotyService
    {
       IList<PrzedmiotDTO> GetAll();
       PrzedmiotDTO Get(Int64 id);
       bool Create(PrzedmiotAddDTO dto);
    }
}