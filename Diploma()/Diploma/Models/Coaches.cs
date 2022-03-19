using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Diploma
{
    public partial class Coaches
    {
        public Coaches()
        {
            Boxers = new HashSet<Boxers>();
        }

        public int CoachId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string SportsTitle { get; set; }
        public int? BoxingClubId { get; set; }

        public virtual BoxingClubs BoxingClub { get; set; }
        public virtual ICollection<Boxers> Boxers { get; set; }
    }
}
