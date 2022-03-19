using Diploma.ViewModels.Boxers;
using System.Collections.Generic;

namespace Diploma.Services
{
    public interface IBoxersServices
    {
        BoxerViewModel GetBoxer(int id);
        IEnumerable<BoxerViewModel> GetAllBoxers();
        EditBoxerViewModel UpdateBoxer(int id, EditBoxerViewModel boxerModel);
        InputBoxerViewModel AddBoxer(InputBoxerViewModel boxerModel);
        DeleteBoxerViewModel DeleteBoxer(int id);
    }
}
