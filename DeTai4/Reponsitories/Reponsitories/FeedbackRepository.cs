using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeTai4.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DeTai4Context _context;

        public FeedbackRepository(DeTai4Context context)
        {
            _context = context;
        }

        // Lấy tất cả phản hồi, bao gồm thông tin đơn hàng
        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            return await _context.Feedbacks
                .Include(f => f.Order) // Bao gồm liên kết với Order nếu cần
                .ToListAsync();
        }

        // Lấy phản hồi theo ID, bao gồm thông tin đơn hàng
        public async Task<Feedback?> GetFeedbackByIdAsync(int feedbackId)
        {
            return await _context.Feedbacks
                .Include(f => f.Order)
                .FirstOrDefaultAsync(f => f.FeedbackId == feedbackId);
        }

        // Thêm phản hồi mới
        public async Task AddFeedbackAsync(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
        }

        // Cập nhật phản hồi
        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync();
        }

        // Xóa phản hồi theo ID
        public async Task DeleteFeedbackAsync(int feedbackId)
        {
            var feedback = await GetFeedbackByIdAsync(feedbackId);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }
    }
}
