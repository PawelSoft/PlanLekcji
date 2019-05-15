using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlanLekcji.DTO;
using PlanLekcji.Repositories;
using System;

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

        public IList<SalaDTO> GetAll()
        {
            return saleRepository.GetAll();
        }

        public bool Delete(Int64 id)
        {
            return saleRepository.Delete(id);
        }
    }
}