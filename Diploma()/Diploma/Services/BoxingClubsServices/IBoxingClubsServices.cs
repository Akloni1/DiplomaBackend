using Diploma.ViewModels.BoxingClubs;
using System.Collections.Generic;

namespace Diploma.Services.BoxingClubsServices
{
    public interface IBoxingClubsServices
    {
        BoxingClubsViewModel GetBoxingClub(int id);
        IEnumerable<BoxingClubsViewModel> GetAllBoxingClubs();
        EditBoxingClubsViewModel UpdateBoxingClub(int id, EditBoxingClubsViewModel boxingClubModel);
        InputBoxingClubsViewModel AddBoxingClub(InputBoxingClubsViewModel boxingClubModel);
        DeleteBoxingClubsViewModel DeleteBoxingClub(int id);
    }
}
