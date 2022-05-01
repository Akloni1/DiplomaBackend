using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Repository.CoachesRepository
{
    public interface ICoachesRepository
    {

        Task<Coaches> GetCoach(int id);
        Task<ICollection<Coaches>> GetAllCoaches();
        Task<Coaches> UpdateCoach(int id, Coaches coachModel);
        Task<Coaches> AddCoach(Coaches coachModel);
        Task<Coaches> DeleteCoach(int id);
    }
}
