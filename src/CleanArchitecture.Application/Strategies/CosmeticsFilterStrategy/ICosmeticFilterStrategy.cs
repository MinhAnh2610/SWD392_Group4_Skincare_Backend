using CleanArchitecture.Application.DTOs.Cosmetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Strategies.CosmeticsFilterStrategy
{
  public interface ICosmeticFilterStrategy
  {
    IQueryable<Cosmetic> ApplyFilter<TProperty>(IQueryable<Cosmetic> query, GetCosmeticsRequest request);
  }

}
