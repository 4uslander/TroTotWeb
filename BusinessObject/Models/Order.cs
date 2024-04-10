using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Qrimage { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public DateTime DateTime { get; set; }
        public int Status { get; set; }
        public virtual User User { get; set; }
    }
}
