using CleanArchitecture.Application.DTOs.Cosmetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Strategies.CosmeticsFilterStrategy
{
  public class GenderFilterStrategy : ICosmeticFilterStrategy
  {
    public IQueryable<Cosmetic> ApplyFilter<TProperty>(IQueryable<Cosmetic> query, GetCosmeticsRequest request)
    {
      if (request.Gender.HasValue)
      {
        return query.Where(c => c.Gender == request.Gender.Value);
      }
      return query;
    }
  }
}
