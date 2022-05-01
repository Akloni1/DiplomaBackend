using Diploma.ViewModels.Coaches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Services.CoachesServices
{
    public interface ICoachesServices
    {
        CoachViewModel GetCoach(int id);
        Task<ICollection<CoachViewModel>> GetAllCoaches();
        EditCoachViewModel UpdateCoaches(int id, EditCoachViewModel coachModel);
        CoachViewModel AddCoach(InputCoachViewModel coachModel);
        DeleteCoachViewModel DeleteCoach(int id);
    }
}
