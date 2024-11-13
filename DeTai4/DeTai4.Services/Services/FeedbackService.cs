using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories;

namespace DeTai4.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        // Lấy tất cả phản hồi
        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            return await _feedbackRepository.GetAllFeedbacksAsync();
        }

        // Lấy phản hồi theo ID
        public async Task<Feedback?> GetFeedbackByIdAsync(int feedbackId)
        {
            return await _feedbackRepository.GetFeedbackByIdAsync(feedbackId);
        }

        // Tạo phản hồi mới
        public async Task CreateFeedbackAsync(Feedback feedback)
        {
            // Thêm logic nếu cần thiết trước khi lưu phản hồi
            await _feedbackRepository.AddFeedbackAsync(feedback);
        }

        // Cập nhật phản hồi
        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            // Thêm logic nếu cần thiết trước khi cập nhật phản hồi
            await _feedbackRepository.UpdateFeedbackAsync(feedback);
        }

        // Xóa phản hồi theo ID
        public async Task DeleteFeedbackAsync(int feedbackId)
        {
            // Thêm logic nếu cần thiết trước khi xóa phản hồi
            await _feedbackRepository.DeleteFeedbackAsync(feedbackId);
        }
    }
}
