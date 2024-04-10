using BusinessLogic.Utils;
using DataAccess.Repositories.OrderRepo;
using System.Threading.Tasks;
using System;
using DataAccess.ViewModels.Orders;
using DataAccess.Enum;
using System.Collections.Generic;

namespace BusinessLogic.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private IOrderRepo _orderRepo;
        private DecodeToken _decodeToken;

        public OrderService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
            _decodeToken = new DecodeToken();
        }

        public async Task CreateOrderAsync(string token, OrderFormModel model)
        {
            try
            {
                string role = _decodeToken.DecodeText(token, "Role");
                if (role.Equals("Admin")) 
                       throw new UnauthorizedAccessException("You do not have permission to do this action!");
                if (role.Equals("Vip"))
                    throw new UnauthorizedAccessException("Bạn đã đăng ký gói nâng cấp!");
                int userId = _decodeToken.Decode(token, "UserId");
                await _orderRepo.CreateOrder(model, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CheckoutOrderAsync(string token, int orderId)
        {
            try
            {
                string role = _decodeToken.DecodeText(token, "Role");
                if (role.Equals("User")) 
                    throw new UnauthorizedAccessException("You do not have permission to do this action!");
                var order = await _orderRepo.GetOrderById(orderId);
                if (order == null) throw new NullReferenceException("Not found any orders!");
                await _orderRepo.CheckoutOrder(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ViewOrder> GetOrderByIdAsync(string token, int orderId)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("Admin"))
                throw new UnauthorizedAccessException("You do not have permission to view this resource!");
            var order = await _orderRepo.GetOrderById(orderId);
            if (order == null) throw new NullReferenceException("Not found any orders!");
            return order;
        }

        public async Task EditOrderAsync(string token, OrderFormModel model, int orderId)
        {
            try
            {
                string role = _decodeToken.DecodeText(token, "Role");
                if (role.Equals("Admin"))
                    throw new UnauthorizedAccessException("You do not have permission to do this action!");
                var order = await _orderRepo.GetOrderById(orderId);
                if (order == null) throw new NullReferenceException("Not found any orders!");
                if (order.Status.Equals(OrderStatus.Checkouted))
                    throw new ArgumentException("Không thể chỉnh sửa khi đã thanh toán!");
                await _orderRepo.EditOrder(model, orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DisableOrderAsync(string token, int orderId)
        {
            try
            {
                string role = _decodeToken.DecodeText(token, "Role");
                if (role.Equals("Admin"))
                    throw new UnauthorizedAccessException("You do not have permission to do this action!");
                var order = await _orderRepo.GetOrderById(orderId);
                if (order == null) throw new NullReferenceException("Not found any orders!");
                await _orderRepo.DisableOrder(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<ViewOrder>> GetOrdersAsync(string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("Admin"))
                throw new UnauthorizedAccessException("You do not have permission to view this resource!");
            int userId = _decodeToken.Decode(token, "UserId");
            var orders = await _orderRepo.GetAllOrders(userId);
            if (orders == null) throw new NullReferenceException("Not found any orders!");
            return orders;
        }

        public async Task<IList<ViewOrder>> GetAllOrdersAsync(string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("User"))
                throw new UnauthorizedAccessException("You do not have permission to view this resource!");
            var orders = await _orderRepo.GetAllOrdersForAdmin();
            if (orders == null) throw new NullReferenceException("Not found any orders!");
            return orders;
        }
    }
}
