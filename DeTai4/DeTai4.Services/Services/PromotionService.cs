using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Repositories;
using DeTai4.Reponsitories.Repositories.Entities;

namespace DeTai4.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<IEnumerable<Promotion>> GetAllPromotionsAsync()
        {
            return await _promotionRepository.GetAllPromotionsAsync();
        }

        public async Task<Promotion?> GetPromotionByIdAsync(int promotionId)
        {
            return await _promotionRepository.GetPromotionByIdAsync(promotionId);
        }

        public async Task CreatePromotionAsync(Promotion promotion)
        {
            // Business logic for creating a promotion can be added here
            await _promotionRepository.AddPromotionAsync(promotion);
        }

        public async Task UpdatePromotionAsync(Promotion promotion)
        {
            // Business logic for updating a promotion can be added here
            await _promotionRepository.UpdatePromotionAsync(promotion);
        }

        public async Task DeletePromotionAsync(int promotionId)
        {
            // Business logic for deleting a promotion can be added here
            await _promotionRepository.DeletePromotionAsync(promotionId);
        }
    }
}
