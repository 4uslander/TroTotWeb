using BusinessObject.Context;
using BusinessObject.Models;
using DataAccess.Enum;
using DataAccess.Repositories.GenericRepo;
using DataAccess.ViewModels.Orders;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace DataAccess.Repositories.OrderRepo
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        public OrderRepo(TroTotDBContext context) : base(context)
        {
        }

        public async Task CreateOrder(OrderFormModel model, int userId)
        {
            var order = new Order() {
                UserId = userId,
                Qrimage = model.Qrimage,
                Price = model.Price,
                Note = model.Note,
                DateTime = System.DateTime.Now.ToLocalTime(),
                Status = (int) OrderStatus.New
            };
            await CreateAsync(order);
        }

        public async Task CheckoutOrder(int orderId)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.OrderId.Equals(orderId));
            order.Status = (int) OrderStatus.Checkouted;
            await UpdateAsync(order);
        }

        public async Task<ViewOrder> GetOrderById(int orderId)
        {
            var query = from o in context.Orders
                        join u in context.Users on o.UserId equals u.UserId
                        where o.OrderId.Equals(orderId) && o.Status != (int) OrderStatus.Disabled
                        select new { o, u };
            ViewOrder order = await query.Select(selector => new ViewOrder
            {
                OrderId = selector.o.OrderId,
                UserId = selector.o.UserId,
                UserName = selector.u.FullName,
                Email = selector.u.Email,
                PhoneNumber = selector.u.PhoneNumber,
                Qrimage = selector.o.Qrimage,
                Price = selector.o.Price,
                Note = selector.o.Note,
                DateTime = selector.o.DateTime,
                DateTimeString = selector.o.DateTime.ToString(),
                Status = (OrderStatus)selector.o.Status,
                StatusString = (selector.o.Status.Equals((int) OrderStatus.New)) ? "Chưa thanh toán" : "Đã thanh toán"
            }).FirstOrDefaultAsync();
            return (order != null) ? order : null;
        }

        public async Task EditOrder(OrderFormModel model, int orderId)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.OrderId.Equals(orderId));
            order.Qrimage = model.Qrimage;
            order.Note = model.Note;
            order.DateTime = System.DateTime.Now.ToLocalTime();
            await UpdateAsync(order);
        }

        public async Task DisableOrder(int orderId)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.OrderId.Equals(orderId));
            order.Status = (int) OrderStatus.Disabled;
            await UpdateAsync(order);
        }

        public async Task<IList<ViewOrder>> GetAllOrders(int userId)
        {
            var query = from o in context.Orders
                        join u in context.Users on o.UserId equals u.UserId
                        where o.Status != (int)OrderStatus.Disabled && o.UserId.Equals(userId)
                        select new { o, u};
            IList<ViewOrder> orders = await query.Select(selector => new ViewOrder
            {
                OrderId = selector.o.OrderId,
                UserId = selector.o.UserId,
                UserName = selector.u.FullName,
                Email = selector.u.Email,
                PhoneNumber = selector.u.PhoneNumber,
                Qrimage = selector.o.Qrimage,
                Price = selector.o.Price,
                Note = selector.o.Note,
                DateTime = selector.o.DateTime,
                DateTimeString = selector.o.DateTime.ToString(),
                Status = (OrderStatus)selector.o.Status,
                StatusString = (selector.o.Status.Equals((int)OrderStatus.New)) ? "Chưa thanh toán" : "Đã thanh toán"
            }).ToListAsync();
            return (orders.Count > 0) ? orders : null;
        }

        public async Task<IList<ViewOrder>> GetAllOrdersForAdmin()
        {
            var query = from o in context.Orders
                        join u in context.Users on o.UserId equals u.UserId
                        where o.Status != (int)OrderStatus.Disabled
                        select new { o, u };
            IList<ViewOrder> orders = await query.OrderByDescending(selector => selector.o.DateTime).Select(selector => new ViewOrder
            {
                OrderId = selector.o.OrderId,
                UserId = selector.o.UserId,
                UserName = selector.u.FullName,
                Email = selector.u.Email,
                PhoneNumber = selector.u.PhoneNumber,
                Qrimage = selector.o.Qrimage,
                Price = selector.o.Price,
                Note = selector.o.Note,
                DateTime = selector.o.DateTime,
                DateTimeString = selector.o.DateTime.ToString(),
                Status = (OrderStatus)selector.o.Status,
                StatusString = (selector.o.Status.Equals((int)OrderStatus.New)) ? "Chưa thanh toán" : "Đã thanh toán"
            }).ToListAsync();
            return (orders.Count > 0) ? orders : null;
        }
    }
}
