using CleanArchitecture.Application.DTOs.Refund;
using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
    public class RefundService : IRefundService
    {
        private readonly IRefundRepository _refundRepository;


        public RefundService(
            IRefundRepository refundRepository
          )
        {
            _refundRepository = refundRepository;
            
        }

      
       
    }
}
