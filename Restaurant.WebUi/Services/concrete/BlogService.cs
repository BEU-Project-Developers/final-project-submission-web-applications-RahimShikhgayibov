using Microsoft.EntityFrameworkCore;
using Restaurant.WebUi.Data;
using Restaurant.WebUi.Models;
using Restaurant.WebUi.Services.interfaces;
using System;

namespace Restaurant.WebUi.Services.concrete
{
    public class BlogService : IBlogService
{
    private readonly ApplicationDbContext _context;

    public BlogService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<BlogPost>> GetAllAsync()
    {
        return await _context.BlogPosts.ToListAsync();
    }

    public async Task<BlogPost?> GetByIdAsync(int id)
    {
        return await _context.BlogPosts.FindAsync(id);
    }

    public async Task AddAsync(BlogPost blogPost)
    {
        await _context.BlogPosts.AddAsync(blogPost);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BlogPost blogPost)
    {
        _context.BlogPosts.Update(blogPost);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var blogPost = await _context.BlogPosts.FindAsync(id);
        if (blogPost != null)
        {
            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();
        }
    }
}

}
