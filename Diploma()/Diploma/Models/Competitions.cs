using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Diploma
{
    public partial class Competitions
    {
        public Competitions()
        {
            CompetitionsBoxers = new HashSet<CompetitionsBoxers>();
            CompetitionsClubs = new HashSet<CompetitionsClubs>();
        }

        public int CompetitionsId { get; set; }
        public string CompetitionsName { get; set; }
        public string CompetitionsAddress { get; set; }

        public virtual ICollection<CompetitionsBoxers> CompetitionsBoxers { get; set; }
        public virtual ICollection<CompetitionsClubs> CompetitionsClubs { get; set; }
    }
}
