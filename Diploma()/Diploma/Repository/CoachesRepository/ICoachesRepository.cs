using System.Collections.Generic;

namespace Diploma.Repository.CoachesRepository
{
    public interface ICoachesRepository
    {

        Coaches GetCoach(int id);
        IEnumerable<Coaches> GetAllCoaches();
        Coaches UpdateCoach(int id, Coaches coachModel);
        Coaches AddCoach(Coaches coachModel);
        Coaches DeleteCoach(int id);
    }
}
