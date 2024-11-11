using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using DeTai4.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync()
        {
            return await _blogRepository.GetAllBlogsAsync();
        }

        public async Task<Blog?> GetBlogByIdAsync(int id)
        {
            return await _blogRepository.GetBlogByIdAsync(id);
        }

        public async Task AddBlogAsync(Blog blog)
        {
            await _blogRepository.AddBlogAsync(blog);
        }

        public async Task UpdateBlogAsync(Blog blog)
        {
            await _blogRepository.UpdateBlogAsync(blog);
        }

        public async Task DeleteBlogAsync(int id)
        {
            await _blogRepository.DeleteBlogAsync(id);
        }
    }
}