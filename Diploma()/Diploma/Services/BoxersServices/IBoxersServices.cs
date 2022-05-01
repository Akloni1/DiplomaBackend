using Diploma.ViewModels.Boxers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Services
{
    public interface IBoxersServices
    {
        Task<BoxerViewModel> GetBoxer(int id);
        Task<ICollection<BoxerViewModel>> GetAllBoxers();
        Task<EditBoxerViewModel> UpdateBoxer(int id, EditBoxerViewModel boxerModel);
        Task<BoxerViewModel> AddBoxer(InputBoxerViewModel boxerModel);
        Task<DeleteBoxerViewModel> DeleteBoxer(int id);
    }
}
