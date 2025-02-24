using Abp.Domain.Repositories;
using Abp.Linq.Expressions;
using CleanArchitecture.Application.DTOs.BatchDto;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
  public class BatchService : IBatchService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IErrorFactory _errorFactory;

    public BatchService(
      IUnitOfWork unitOfWork, IErrorFactory errorFactory)
    {
      _unitOfWork = unitOfWork;
      _errorFactory = errorFactory;
    }

    public async Task<Result<BatchResponse>> CreateBatch(BatchCreateRequest batch)
    {
      var orgbatch = batch.Adapt<Batch>();
      orgbatch.Cosmetic = new Cosmetic { Id = batch.CosmeticId };
      orgbatch.CosmeticId = batch.CosmeticId;
      orgbatch.Quantity = batch.Quantity;
      orgbatch.ExportedDate = batch.ExportedDate;
      orgbatch.ManufactureDate = batch.ManufactureDate;
      orgbatch.ExpirationDate = batch.ExpirationDate;

      _unitOfWork.Batches.Create(orgbatch);
      var output = orgbatch.Adapt<BatchResponse>();
      return Result<BatchResponse>.Success(output, StatusCodes.Status201Created);
    }

    public async Task<Result<BatchResponse>> DeleteBatch(Guid id)
    {
      var existbatch = await _unitOfWork.Batches.GetByIdAsync(id);
      if (existbatch == null)
      {
        return Result<BatchResponse>.Failure([BatchErrors.BatchNotFound], StatusCodes.Status404NotFound);
      }

      _unitOfWork.Batches.Remove(existbatch);
      var isSaved = await _unitOfWork.CompleteAsync();
      if (!isSaved)
      {
        var error = _errorFactory.CreateDatabaseError("Batch");
        return Result<BatchResponse>.Failure([error.err], error.statusCode);
      }

      var output = existbatch.Adapt<BatchResponse>();
      return Result<BatchResponse>.Success(output, StatusCodes.Status200OK);
    }

    public async Task<Result<List<BatchResponse>>> GetAllBatches()
    {
      var batches = await _unitOfWork.Batches.GetAllAsync();
      if (batches != null)
      {
        var batchesResponse = batches.Adapt<List<BatchResponse>>();
        return Result<List<BatchResponse>>.Success(batchesResponse, StatusCodes.Status200OK);
      }

      return Result<List<BatchResponse>>.Failure([BatchErrors.BatchNotFound], StatusCodes.Status404NotFound);
    }

    public async Task<Result<BatchResponse>> GetBatchById(Guid id)
    {
      var batch = await _unitOfWork.Batches.GetByIdAsync(id);

      if (batch is null)
      {
        var error = _errorFactory.CreateNotFoundError("Batch");
        return Result<BatchResponse>.Failure([error.err], error.statusCode);
      }
      
      return Result<BatchResponse>.Success(batch.Adapt<BatchResponse>(), StatusCodes.Status200OK);
    }

    public async Task<Result<List<BatchResponse>>> GetBatchesByCosmeticId(Guid cosId)
    {
      var batch = await _unitOfWork.Batches.GetListByAnyId(e => e.CosmeticId == cosId, 2);
      if (batch != null)
      {
        var batchesResponse = batch.Adapt<List<BatchResponse>>();
        return Result<List<BatchResponse>>.Success(batchesResponse, StatusCodes.Status200OK);
      }
      else
      {
        return Result<List<BatchResponse>>.Failure([BatchErrors.BatchNotFound], StatusCodes.Status404NotFound);
      }
    }

    public async Task<Result<BatchResponse>> UpdateBatch(BatchUpdateRequest batch, Guid id)
    {
      var existbatch = await _unitOfWork.Batches.GetByIdAsync(id);
      if (existbatch == null)
      {
        return Result<BatchResponse>.Failure([BatchErrors.BatchNotFound], StatusCodes.Status404NotFound);
      }

      // Only update if the new value is NOT null
      existbatch.Quantity = batch.Quantity != default ? batch.Quantity : existbatch.Quantity;
      existbatch.ExportedDate = batch.ExportedDate != default ? batch.ExportedDate : existbatch.ExportedDate;

      _unitOfWork.Batches.Update(existbatch);
      var isSaved = await _unitOfWork.CompleteAsync();
      if (!isSaved)
      {
        var error = _errorFactory.CreateDatabaseError("Batch");
        return Result<BatchResponse>.Failure([error.err], error.statusCode);
      }

      var output = existbatch.Adapt<BatchResponse>();

      return Result<BatchResponse>.Success(output, StatusCodes.Status200OK);
    }

    public async Task<Result<List<BatchResponse>>> GetBatchesByDateRangeAsync(DateOnly startDate, DateOnly endDate,
      DateSearchType searchTypes)
    {
      var predicate = PredicateBuilder.New<Batch>(false);

      if (searchTypes.HasFlag(DateSearchType.Exported))
      {
        predicate = predicate.Or(b => b.ExportedDate >= startDate && b.ExportedDate <= endDate);
      }

      if (searchTypes.HasFlag(DateSearchType.Manufacture))
      {
        predicate = predicate.Or(b => b.ManufactureDate >= startDate && b.ManufactureDate <= endDate);
      }

      if (searchTypes.HasFlag(DateSearchType.Expiration))
      {
        predicate = predicate.Or(b => b.ExpirationDate >= startDate && b.ExpirationDate <= endDate);
      }

      var batches = await _unitOfWork.Batches.GetListByAnyId(predicate, 2);
      var batchesResponse = batches.Adapt<List<BatchResponse>>();
      return Result<List<BatchResponse>>.Success(batchesResponse, StatusCodes.Status200OK);
    }
  }
}