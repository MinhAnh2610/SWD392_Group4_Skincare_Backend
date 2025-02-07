using CleanArchitecture.Application.DTOs.Payment;
using CleanArchitecture.Application.DTOs.Refund;
using CleanArchitecture.Application.DTOs.RefundItem;
using CleanArchitecture.Application.ServiceContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.RepositoryContracts;
using CleanArchitecture.Domain.RepositoryContracts.Base;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
  

        public PaymentService(
            IPaymentRepository paymentRepository
            )
        {
            _paymentRepository = paymentRepository;
            
        }

     
    }
}