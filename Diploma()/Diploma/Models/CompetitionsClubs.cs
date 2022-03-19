using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Diploma
{
    public partial class CompetitionsClubs
    {
        public int CompetitionsId { get; set; }
        public int BoxingClubId { get; set; }

        public virtual BoxingClubs BoxingClub { get; set; }
        public virtual Competitions Competitions { get; set; }
    }
}
