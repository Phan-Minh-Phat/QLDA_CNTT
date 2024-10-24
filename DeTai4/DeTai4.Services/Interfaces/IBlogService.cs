using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Repositories.Entities;

namespace DeTai4.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllPostsAsync();
        Task<Blog?> GetPostByIdAsync(int postId);
        Task CreatePostAsync(Blog blog);
        Task UpdatePostAsync(Blog blog);
        Task DeletePostAsync(int postId);
    }
}
