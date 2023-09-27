using ReserveOrderFinal.Models;

namespace ReserveOrderFinal.Interface
{
    public interface IEmailService
    {
        Task<int> SendEmailAsync(OrderDetails payload);
    }
}