using System.Collections.Generic;

namespace Diploma.Repository.BoxingClubsRepository
{
    public interface IBoxingClubsRepository
    {
        BoxingClubs GetBoxingClub(int id);
        IEnumerable<BoxingClubs> GetAllBoxingClubs();
        BoxingClubs UpdateBoxingClub(int id, BoxingClubs boxerModel);
        BoxingClubs AddBoxingClub(BoxingClubs boxerModel);
        BoxingClubs DeleteBoxingClub(int id);
    }
}
