using Restaurant.WebUi.Models;

namespace Restaurant.WebUi.Services.interfaces
{
    public interface IBlogService
{
    Task<List<BlogPost>> GetAllAsync();
    Task<BlogPost?> GetByIdAsync(int id);
    Task AddAsync(BlogPost blogPost);
    Task UpdateAsync(BlogPost blogPost);
    Task DeleteAsync(int id);
}

}
