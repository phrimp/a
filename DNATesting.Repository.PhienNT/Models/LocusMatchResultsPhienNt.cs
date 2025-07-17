using System;
using System.Collections.Generic;

namespace DNATesting.Repository.PhienNT;

public partial class LocusMatchResultsPhienNt
{
    public int PhienNtid { get; set; }

    public int TestId { get; set; }

    public int LocusId { get; set; }

    public bool? IsMatch { get; set; }

    public decimal? MatchScore { get; set; }

    public DateTime? EvaluatedAt { get; set; }

    public string EvaluatorNotes { get; set; }

    public virtual LociPhienNt Locus { get; set; }

    public virtual DnaTestsPhienNt Test { get; set; }
}
