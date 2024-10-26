using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;

namespace DeTai4.Services
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetAllPromotionsAsync();
        Task<Promotion?> GetPromotionByIdAsync(int promotionId);
        Task CreatePromotionAsync(Promotion promotion);
        Task UpdatePromotionAsync(Promotion promotion);
        Task DeletePromotionAsync(int promotionId);
    }
}
