using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Repository.CoachesRepository
{
    public interface ICoachesRepository
    {

        Coaches GetCoach(int id);
        Task<ICollection<Coaches>> GetAllCoaches();
        Coaches UpdateCoach(int id, Coaches coachModel);
        Coaches AddCoach(Coaches coachModel);
        Coaches DeleteCoach(int id);
    }
}
