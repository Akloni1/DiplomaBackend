using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Repository.BoxingClubsRepository
{
    public interface IBoxingClubsRepository
    {
        Task<BoxingClubs> GetBoxingClub(int id);
        Task<IEnumerable<BoxingClubs>> GetAllBoxingClubs();
        Task<BoxingClubs> UpdateBoxingClub(int id, BoxingClubs boxerModel);
        Task<BoxingClubs> AddBoxingClub(BoxingClubs boxerModel);
        Task<BoxingClubs> DeleteBoxingClub(int id);
    }
}
