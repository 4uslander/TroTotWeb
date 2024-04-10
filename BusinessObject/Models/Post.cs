using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Post
    {
        public Post()
        {
            Actions = new HashSet<Action>();
        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public decimal? Price { get; set; }
        public DateTime DateTime { get; set; }
        public int? Area { get; set; }
        public int? Baths { get; set; }
        public int? Beds { get; set; }
        public int Status { get; set; }
        public int? NumberOfPeople { get; set; }
        public int? Wifi { get; set; }
        public int CategoryId { get; set; }
        public int? TypeId { get; set; }
        public int UserId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Type Type { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Action> Actions { get; set; }
    }
}
