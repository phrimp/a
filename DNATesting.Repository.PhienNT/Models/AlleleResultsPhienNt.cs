using System;
using System.Collections.Generic;

namespace DNATesting.Repository.PhienNT;

public partial class AlleleResultsPhienNt
{
    public int PhienNtid { get; set; }

    public int TestId { get; set; }

    public int LocusId { get; set; }

    public int ProfileThinhLcid { get; set; }

    public string Role { get; set; }

    public int Allele1 { get; set; }

    public int Allele2 { get; set; }

    public decimal? ConfidenceScore { get; set; }

    public bool? IsOutlier { get; set; }

    public DateTime? TestedAt { get; set; }

    public string Comments { get; set; }

    public virtual LociPhienNt Locus { get; set; }

    public virtual ProfileThinhLc ProfileThinhLc { get; set; }

    public virtual DnaTestsPhienNt Test { get; set; }
}
