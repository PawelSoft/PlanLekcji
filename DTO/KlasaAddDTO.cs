using System;

namespace PlanLekcji.DTO
{
  public class KlasaAddDTO
  {
    public string Nazwa { get; set; }
    public Int32 Rocznik { get; set; }
    public Int64 WychowawcaId { get; set; }
  }
}
