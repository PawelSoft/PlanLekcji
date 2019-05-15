using System;
using System.Collections.Generic;
using PlanLekcji.DTO;

namespace PlanLekcji.Repositories
{
    public interface IPrzedmiotyRepository
    {
        IList<PrzedmiotDTO> GetAll();
       PrzedmiotDTO Get(Int64 id);
       bool Create(PrzedmiotAddDTO dto);

       bool SprawdzCzyNazwaIstnieje(string nazwa);
    }
}