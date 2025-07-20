using HotChocolate;
using System;
using System.Collections.Generic;

namespace DNATesting.Repository.PhienNT;

public partial class LociPhienNt
{
    public int PhienNtid { get; set; }

    public string Name { get; set; }

    public bool? IsCodis { get; set; }

    public string Description { get; set; }

    public decimal? MutationRate { get; set; }

    public DateTime? CreatedAt { get; set; }

    [GraphQLIgnore]
    public virtual ICollection<AlleleResultsPhienNt> AlleleResultsPhienNts { get; set; } = new List<AlleleResultsPhienNt>();

    [GraphQLIgnore]
    public virtual ICollection<LocusMatchResultsPhienNt> LocusMatchResultsPhienNts { get; set; } = new List<LocusMatchResultsPhienNt>();
}
