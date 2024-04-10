using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class PremiumRegister
    {
        public int UserId { get; set; }
        public int PremiumTypeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual PremiumType PremiumType { get; set; }
    }
}
