namespace Restaurant.WebUi.ViewModel
{
public class BlogPostViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string DateFormatted { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public int CommentCount { get; set; }
}


}
