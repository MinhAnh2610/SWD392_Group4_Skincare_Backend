using CleanArchitecture.Application.DTOs.Cosmetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Strategies.CosmeticsFilterStrategy
{
  public class SkinTypeFilterStrategy : ICosmeticFilterStrategy
  {
    public IQueryable<Cosmetic> ApplyFilter<TProperty>(IQueryable<Cosmetic> query, GetCosmeticsRequest request)
    {
      if (request.SkinTypeId.HasValue)
      {
        return query.Where(c => c.SkinTypeId == request.SkinTypeId.Value);
      }
      return query;
    }
  }
}
