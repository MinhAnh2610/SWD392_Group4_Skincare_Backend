using CleanArchitecture.Application.DTOs.BatchDto;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Mapper
{
  public class BatchMap
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<Batch, BatchResponse>();
      config.NewConfig<BatchUpdateRequest, BatchResponse>();
      config.NewConfig<BatchCreateRequest, BatchResponse>();
      config.NewConfig<Batch, BatchCreateRequest>();
    }
  }
}
