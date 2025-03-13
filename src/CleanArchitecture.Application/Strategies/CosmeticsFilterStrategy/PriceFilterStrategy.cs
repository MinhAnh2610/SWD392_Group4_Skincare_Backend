using CleanArchitecture.Application.DTOs.Cosmetic;
using CleanArchitecture.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CleanArchitecture.Application.Strategies.CosmeticsFilterStrategy
{
  public class PriceFilterStrategy : ICosmeticFilterStrategy
  {
    private readonly IUnitOfWork _unitOfWork;

    public PriceFilterStrategy(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IQueryable<Cosmetic> ApplyFilter<TProperty>(IQueryable<Cosmetic> query, GetCosmeticsRequest request)
    {
      if (!request.MinPrice.HasValue && !request.MaxPrice.HasValue)
      {
        return query;
      }

      // Get current date for price calculations
      var currentDate = DateTime.UtcNow;

      // Join with CosmeticPrices and Events to get the actual selling price
      var filteredQuery = from cosmetic in query
                          join cp in _unitOfWork.CosmeticPrices.GetQueryable() on cosmetic.Id equals cp.CosmeticId
                          join e in _unitOfWork.Events.GetQueryable() on cp.EventId equals e.Id into eventGroup
                          from evt in eventGroup.DefaultIfEmpty()
                          let sellingPrice = cp.OriginalPrice * (evt != null ? (100m - evt.DiscountPercentage) / 100m : 1m)
                          where (!request.MinPrice.HasValue || sellingPrice >= request.MinPrice.Value) &&
                                (!request.MaxPrice.HasValue || sellingPrice <= request.MaxPrice.Value)
                          select cosmetic;

      return filteredQuery.Distinct();
    }
  }
}