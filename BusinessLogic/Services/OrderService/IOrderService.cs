using DataAccess.ViewModels.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services.OrderService
{
    public interface IOrderService
    {
        Task CreateOrderAsync(string token, OrderFormModel model);
        Task CheckoutOrderAsync(string token, int orderId);
        Task<ViewOrder> GetOrderByIdAsync(string token, int orderId);
        Task EditOrderAsync(string token, OrderFormModel model, int orderId);
        Task DisableOrderAsync(string token, int orderId);
        Task<IList<ViewOrder>> GetOrdersAsync(string token);
        Task<IList<ViewOrder>> GetAllOrdersAsync(string token);
    }
}
