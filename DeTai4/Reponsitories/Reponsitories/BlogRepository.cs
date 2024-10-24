using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeTai4.Repositories.Implementations
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DbContext _context;

        public BlogRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllPostsAsync()
        {
            return await _context.Set<Blog>().ToListAsync();
        }

        public async Task<Blog?> GetPostByIdAsync(int postId)
        {
            return await _context.Set<Blog>().FindAsync(postId);
        }

        public async Task CreatePostAsync(Blog blog)
        {
            await _context.Set<Blog>().AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Blog blog)
        {
            _context.Set<Blog>().Update(blog);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int postId)
        {
            var blog = await GetPostByIdAsync(postId);
            if (blog != null)
            {
                _context.Set<Blog>().Remove(blog);
                await _context.SaveChangesAsync();
            }
        }
    }
}
