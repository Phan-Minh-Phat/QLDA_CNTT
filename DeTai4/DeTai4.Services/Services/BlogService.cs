using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using DeTai4.Services.Interfaces;

namespace DeTai4.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IEnumerable<Blog>> GetAllPostsAsync()
        {
            return await _blogRepository.GetAllPostsAsync();
        }

        public async Task<Blog?> GetPostByIdAsync(int postId)
        {
            return await _blogRepository.GetPostByIdAsync(postId);
        }

        public async Task CreatePostAsync(Blog blog)
        {
            // Business logic (if any) before creating the post
            await _blogRepository.CreatePostAsync(blog);
        }

        public async Task UpdatePostAsync(Blog blog)
        {
            // Business logic (if any) before updating the post
            await _blogRepository.UpdatePostAsync(blog);
        }

        public async Task DeletePostAsync(int postId)
        {
            // Business logic (if any) before deleting the post
            await _blogRepository.DeletePostAsync(postId);
        }
    }
}
