using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Type
    {
        public Type()
        {
            Posts = new HashSet<Post>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
