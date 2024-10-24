using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Entities;
using DeTai4.Repositories;
using DeTai4.Repositories.Entities;

namespace DeTai4.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            return await _feedbackRepository.GetAllFeedbacksAsync();
        }

        public async Task<Feedback?> GetFeedbackByIdAsync(int feedbackId)
        {
            return await _feedbackRepository.GetFeedbackByIdAsync(feedbackId);
        }

        public async Task CreateFeedbackAsync(Feedback feedback)
        {
            // Business logic for creating feedback can be added here
            await _feedbackRepository.AddFeedbackAsync(feedback);
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            // Business logic for updating feedback can be added here
            await _feedbackRepository.UpdateFeedbackAsync(feedback);
        }

        public async Task DeleteFeedbackAsync(int feedbackId)
        {
            // Business logic for deleting feedback can be added here
            await _feedbackRepository.DeleteFeedbackAsync(feedbackId);
        }
    }
}
