using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Diploma
{
    public partial class Boxers
    {
        public Boxers()
        {
            CompetitionsBoxers = new HashSet<CompetitionsBoxers>();
        }

        public int BoxerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Weight { get; set; }
        public double? TrainingExperience { get; set; }
        public int? NumberOfFightsHeld { get; set; }
        public int? NumberOfWins { get; set; }
        public string Discharge { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int? CoachId { get; set; }
        public int? BoxingClubId { get; set; }

        public virtual BoxingClubs BoxingClub { get; set; }
        public virtual Coaches Coach { get; set; }
        public virtual ICollection<CompetitionsBoxers> CompetitionsBoxers { get; set; }

        public static implicit operator Boxers(EntityEntry<Boxers> v)
        {
            throw new NotImplementedException();
        }
    }
}
