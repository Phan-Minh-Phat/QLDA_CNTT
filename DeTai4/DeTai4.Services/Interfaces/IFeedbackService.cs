using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;

namespace DeTai4.Services
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacksAsync();
        Task<Feedback?> GetFeedbackByIdAsync(int feedbackId);
        Task CreateFeedbackAsync(Feedback feedback);
        Task UpdateFeedbackAsync(Feedback feedback);
        Task DeleteFeedbackAsync(int feedbackId);
    }
}
