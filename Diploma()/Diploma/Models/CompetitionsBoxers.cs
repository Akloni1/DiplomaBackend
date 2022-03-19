using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Diploma
{
    public partial class CompetitionsBoxers
    {
        public int CompetitionsId { get; set; }
        public int BoxerId { get; set; }

        public virtual Boxers Boxer { get; set; }
        public virtual Competitions Competitions { get; set; }
    }
}
