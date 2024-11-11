using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Pages.Home
{
    public class BlogModel : PageModel
    {
        private readonly IBlogService _blogService;

        public BlogModel(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public List<Blog> BlogPosts { get; set; } = new List<Blog>();

        public async Task OnGetAsync()
        {
            var posts = await _blogService.GetAllBlogsAsync();
            BlogPosts = posts != null ? new List<Blog>(posts) : new List<Blog>();
        }
    }
}