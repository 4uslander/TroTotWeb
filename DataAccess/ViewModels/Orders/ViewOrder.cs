using DataAccess.Enum;
using System;

namespace DataAccess.ViewModels.Orders
{
    public class ViewOrder
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Qrimage { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public DateTime DateTime { get; set; }
        public string DateTimeString { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusString { get; set; }
    }
}
