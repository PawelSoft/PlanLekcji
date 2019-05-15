using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlanLekcji.DTO;
using PlanLekcji.Repositories;

namespace PlanLekcji.Services
{
    public class PrzedmiotyService : IPrzedmiotyService
    {
        IConfiguration configuration;
        IPrzedmiotyRepository przedmiotyRepository;
        public PrzedmiotyService(IConfiguration configuration, IPrzedmiotyRepository przedmiotyRepository)
        {
            this.configuration = configuration;
            this.przedmiotyRepository = przedmiotyRepository;
        }

        public bool Create(PrzedmiotAddDTO dto)
        {
            if (przedmiotyRepository.SprawdzCzyNazwaIstnieje(dto.Nazwa))
            {
                return false;
            }
            else
                return przedmiotyRepository.Create(dto);
        }

        public PrzedmiotDTO Get(long id)
        {
           return przedmiotyRepository.Get(id); 
        }

        public IList<PrzedmiotDTO> GetAll()
        {
            return przedmiotyRepository.GetAll();
        }
    }
}