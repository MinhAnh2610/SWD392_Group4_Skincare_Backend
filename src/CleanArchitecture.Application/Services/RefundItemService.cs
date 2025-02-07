using CleanArchitecture.Application.DTOs.RefundItem;
using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
    public class RefundItemService : IRefundItemService
    {
        private readonly IRefundItemRepository _refundItemRepository;
 

        public RefundItemService(
            IRefundItemRepository refundItemRepository
           )
        {
            _refundItemRepository = refundItemRepository;
           
        }

       
    }
}
