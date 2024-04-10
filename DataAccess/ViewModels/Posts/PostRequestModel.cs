using DataAccess.Enum;

namespace DataAccess.ViewModels.Posts
{
    public class PostRequestModel
    {
        public string? Address { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? People { get; set; }
        public PostStatus Status { get; set; }
    }
}
