using CleanArchitecture.Application.DTOs.Cosmetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Strategies.CosmeticsFilterStrategy
{
  public class NameFilterStrategy : ICosmeticFilterStrategy
  {
    public IQueryable<Cosmetic> ApplyFilter<TProperty>(IQueryable<Cosmetic> query, GetCosmeticsRequest request)
    {
      if (!string.IsNullOrWhiteSpace(request.Name))
      {
        return query.Where(c => c.Name.ToLower().Contains(request.Name.ToLower()));
      }
      return query;
    }
  }
}