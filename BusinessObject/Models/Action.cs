using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Action
    {
        public int ActionId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public int Status { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
