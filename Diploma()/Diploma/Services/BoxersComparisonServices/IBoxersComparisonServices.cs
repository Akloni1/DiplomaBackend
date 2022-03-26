using Diploma.ViewModels.Boxers;
using System.Collections.Generic;

namespace Diploma.Services.BoxersComparisonServices
{
    public interface IBoxersComparisonServices
    {
        dynamic BoxersComparison(IEnumerable<BoxerViewModel> boxersConcat, double ratioAge, double ratioWeight, double ratioNumberOfFightsHeld, double ratioNumberOfWins, double ratioTrainingExperience);
    }
}
