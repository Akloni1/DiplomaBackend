using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Diploma
{
    public partial class BoxingClubs
    {
        public BoxingClubs()
        {
            Boxers = new HashSet<Boxers>();
            Coaches = new HashSet<Coaches>();
            CompetitionsClubs = new HashSet<CompetitionsClubs>();
            Admin = new HashSet<Admin>();
        }

        public int BoxingClubId { get; set; }
        public string ClubName { get; set; }
        public string ClubAddress { get; set; }

        public virtual ICollection<Boxers> Boxers { get; set; }
        public virtual ICollection<Coaches> Coaches { get; set; }
        public virtual ICollection<CompetitionsClubs> CompetitionsClubs { get; set; }
        public virtual ICollection<Admin> Admin { get; set; }
    }
}
