using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlanLekcji.DTO;
using PlanLekcji.Repositories;

namespace PlanLekcji.Services
{
    public class SaleService : ISaleService
    {
        IConfiguration configuration;
        ISaleRepository saleRepository;
        public SaleService(IConfiguration configuration, ISaleRepository saleRepository)
        {
            this.configuration = configuration;
            this.saleRepository = saleRepository;
        }

        public SalaDTO Get(long id)
        {
            return saleRepository.Get(id); 
        }

        public IList<SalaDTO> GetAll()
        {
            return saleRepository.GetAll();
        }

        public string Delete(Int64 id)
        {
            if (saleRepository.SprawdzCzyIdIstnieje(id))
            {
                if (saleRepository.Delete(id))
                    return null;
                else
                    return "nie udało się usunąć";
            }
            else
            {
                return "obiekt nie istnieje";
            }
        }
    }
}