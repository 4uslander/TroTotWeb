using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class PremiumType
    {
        public PremiumType()
        {
            PremiumRegisters = new HashSet<PremiumRegister>();
        }

        public int PremiumTypeId { get; set; }
        public string PremiumName { get; set; }

        public virtual ICollection<PremiumRegister> PremiumRegisters { get; set; }
    }
}
