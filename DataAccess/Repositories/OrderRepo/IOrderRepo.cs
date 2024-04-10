using DataAccess.ViewModels.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.OrderRepo
{
    public interface IOrderRepo
    {
        Task CreateOrder(OrderFormModel model, int userId);
        Task CheckoutOrder(int orderId);
        Task<ViewOrder> GetOrderById(int orderId);
        Task EditOrder(OrderFormModel model, int orderId);
        Task DisableOrder(int orderId);
        Task<IList<ViewOrder>> GetAllOrders(int userId);
        Task<IList<ViewOrder>> GetAllOrdersForAdmin();
    }
}
