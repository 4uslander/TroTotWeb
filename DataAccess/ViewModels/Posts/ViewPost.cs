using DataAccess.Enum;
using System;

namespace DataAccess.ViewModels.Posts
{
    public class ViewPost
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public decimal? Price { get; set; }
        public DateTime DateTime { get; set; }
        public string DateTimeString { get; set; }
        public int? Area { get; set; }
        public int? Baths { get; set; }
        public int? Beds { get; set; }
        public PostStatus Status { get; set; }
        public string StatusString { get; set; }
        public int? NumberOfPeople { get; set; }
        public int? Wifi { get; set; }
        public int CategoryId { get; set; }
        public int? TypeId { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public string TypeName { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
    }
}
