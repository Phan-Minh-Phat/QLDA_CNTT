using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllBlogsAsync();
        Task<Blog?> GetBlogByIdAsync(int id);
        Task AddBlogAsync(Blog blog);
        Task UpdateBlogAsync(Blog blog);
        Task DeleteBlogAsync(int id);
    }
}