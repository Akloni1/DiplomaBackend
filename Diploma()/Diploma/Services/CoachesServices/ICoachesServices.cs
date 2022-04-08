using Diploma.ViewModels.Coaches;
using System.Collections.Generic;

namespace Diploma.Services.CoachesServices
{
    public interface ICoachesServices
    {
        CoachViewModel GetCoach(int id);
        IEnumerable<CoachViewModel> GetAllCoaches();
        EditCoachViewModel UpdateCoaches(int id, EditCoachViewModel coachModel);
        CoachViewModel AddCoach(InputCoachViewModel coachModel);
        DeleteCoachViewModel DeleteCoach(int id);
    }
}
