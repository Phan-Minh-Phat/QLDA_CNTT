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

        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            return await _context.Feedbacks
                .Include(f => f.Order)
                .ToListAsync();
        }

        public async Task<Feedback?> GetFeedbackByIdAsync(int feedbackId)
        {
            return await _context.Feedbacks
                .Include(f => f.Order)
                .FirstOrDefaultAsync(f => f.FeedbackId == feedbackId);
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync();
        }

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
