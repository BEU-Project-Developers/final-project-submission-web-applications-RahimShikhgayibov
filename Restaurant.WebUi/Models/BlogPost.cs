namespace Restaurant.WebUi.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Category { get; set; } = null!;
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int CommentCount { get; set; }
    }


}
