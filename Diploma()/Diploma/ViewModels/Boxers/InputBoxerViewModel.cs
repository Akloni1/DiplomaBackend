using System;

namespace Diploma.ViewModels.Boxers
{
    public class InputBoxerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public double? TrainingExperience { get; set; }
        public int? NumberOfFightsHeld { get; set; }
        public int? NumberOfWins { get; set; }
        public string Discharge { get; set; }
        public int? CoachId { get; set; }
        public int? BoxingClubId { get; set; }
    }
}