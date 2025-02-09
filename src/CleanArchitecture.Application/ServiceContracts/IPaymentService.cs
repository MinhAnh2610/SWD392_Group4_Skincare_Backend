using CleanArchitecture.Application.DTOs.Payment;

namespace CleanArchitecture.Application.ServiceContracts
{
    public interface IPaymentService
    {
        Task<Result<List<PaymentResponse>>> GetAllPaymentsAsync();
    }
}