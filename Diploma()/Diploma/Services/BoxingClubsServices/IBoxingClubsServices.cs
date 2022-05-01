using Diploma.ViewModels.BoxingClubs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Services.BoxingClubsServices
{
    public interface IBoxingClubsServices
    {
        Task<BoxingClubsViewModel> GetBoxingClub(int id);
        Task<IEnumerable<BoxingClubsViewModel>> GetAllBoxingClubs();
        Task<EditBoxingClubsViewModel> UpdateBoxingClub(int id, EditBoxingClubsViewModel boxingClubModel);
        Task<InputBoxingClubsViewModel> AddBoxingClub(InputBoxingClubsViewModel boxingClubModel);
        Task<DeleteBoxingClubsViewModel> DeleteBoxingClub(int id);
    }
}
