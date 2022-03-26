using Diploma.ViewModels.Boxers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diploma.Services.BoxersComparisonServices
{
    public class BoxersComparisonServices: IBoxersComparisonServices
    {
      
        public dynamic BoxersComparison(IEnumerable<BoxerViewModel> boxersConcat, double ratioAge, double ratioWeight, double ratioNumberOfFightsHeld, double ratioNumberOfWins, double ratioTrainingExperience)
        {
            DateTime timeNow = DateTime.Now;

            List<int> idDistributed = new List<int>();
            List<BoxersViewModel> res = new List<BoxersViewModel>();

            foreach (var boxer1 in boxersConcat)
            {

                if (idDistributed.Contains(boxer1.BoxerId))
                {
                    continue;
                }

                var dateOfBirth1 = (DateTime)boxer1.DateOfBirth;
                double age1 = timeNow.Year - dateOfBirth1.Year;
                foreach (var boxer2 in boxersConcat)
                {
                    if (idDistributed.Contains(boxer2.BoxerId))
                    {
                        continue;
                    }
                    if (idDistributed.Contains(boxer1.BoxerId))
                    {
                        continue;
                    }

                    var dateOfBirth2 = (DateTime)boxer2.DateOfBirth;
                    double age2 = timeNow.Year - dateOfBirth2.Year;
                    if (boxer1.BoxerId == boxer2.BoxerId) continue;

                    if (age1 / 100d * ratioAge >= Math.Abs(age1 - age2))
                    {
                        if ((double)boxer1.Weight / 100d * ratioWeight >= Math.Abs((int)boxer1.Weight - (int)boxer2.Weight))
                        {
                            if ((double)boxer1.NumberOfFightsHeld / 100d * ratioNumberOfFightsHeld >= Math.Abs((int)boxer1.NumberOfFightsHeld - (int)boxer2.NumberOfFightsHeld))
                            {
                                if ((double)boxer1.NumberOfWins / 100d * ratioNumberOfWins >= Math.Abs((int)boxer1.NumberOfWins - (int)boxer2.NumberOfWins))
                                {
                                    if ((double)boxer1.TrainingExperience / 100d * ratioTrainingExperience >= Math.Abs((double)boxer1.TrainingExperience - (double)boxer2.TrainingExperience))
                                    {
                                        if (((string)boxer1.Discharge).Equals((string)boxer2.Discharge))
                                        {
                                            BoxersViewModel couple = new BoxersViewModel();
                                            couple.boxer1 = boxer1;
                                            couple.boxer2 = boxer2;
                                            res.Add(couple);


                                            idDistributed.Add(boxer1.BoxerId);
                                            idDistributed.Add(boxer2.BoxerId);


                                          
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


            }

            List<BoxerViewModel> notPaired = boxersConcat.ToList();
            foreach (int idDist in idDistributed)
            {
                var model = (notPaired.Where(a => a.BoxerId == idDist).First());
                notPaired.Remove(model);

            }

            var final = new
            {
                res,

                notPaired

            };
            return final;
        }
    }
}
