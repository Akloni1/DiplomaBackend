using Diploma.ViewModels.Coaches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Services.CoachesServices
{
    public interface ICoachesServices
    {
        Task<CoachViewModel> GetCoach(int id);
        Task<ICollection<CoachViewModel>> GetAllCoaches();
        Task<EditCoachViewModel> UpdateCoaches(int id, EditCoachViewModel coachModel);
        Task<CoachViewModel> AddCoach(InputCoachViewModel coachModel);
        Task<DeleteCoachViewModel> DeleteCoach(int id);
    }
}
