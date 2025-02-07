using CleanArchitecture.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ServiceContracts
{
    public interface IPaymentService
    {
        Task<Result<List<PaymentResponse>>> GetAllPaymentsAsync();
        Task<Result<PaymentResponse>> GetPaymentByIdAsync(Guid id);
        Task<Result<PaymentResponse>> CreatePaymentAsync(CreatePaymentRequest request);
        Task<Result<PaymentResponse>> UpdatePaymentAsync(UpdatePaymentRequest request);
        Task<Result<string>> DeletePaymentAsync(Guid id);
    }
}
