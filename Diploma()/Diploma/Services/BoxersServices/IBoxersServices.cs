using Diploma.ViewModels.Boxers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Services
{
    public interface IBoxersServices
    {
        BoxerViewModel GetBoxer(int id);
        Task<ICollection<BoxerViewModel>> GetAllBoxers();
        EditBoxerViewModel UpdateBoxer(int id, EditBoxerViewModel boxerModel);
        BoxerViewModel AddBoxer(InputBoxerViewModel boxerModel);
        DeleteBoxerViewModel DeleteBoxer(int id);
    }
}
