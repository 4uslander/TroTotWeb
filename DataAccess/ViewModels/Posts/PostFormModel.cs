namespace DataAccess.ViewModels.Posts
{
    public class PostFormModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public decimal? Price { get; set; }
        public int? Area { get; set; }
        public int? Baths { get; set; }
        public int? Beds { get; set; }
        public int? NumberOfPeople { get; set; }
        public int? Wifi { get; set; }
        public int CategoryId { get; set; }
        public int? TypeId { get; set; }
    }
}
