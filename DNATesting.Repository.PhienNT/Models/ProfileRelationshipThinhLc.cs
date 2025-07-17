using System;
using System.Collections.Generic;

namespace DNATesting.Repository.PhienNT;

public partial class ProfileRelationshipThinhLc
{
    public int ProfileRelationshipThinhLcid { get; set; }

    public int ProfileThinhLcid1 { get; set; }

    public int ProfileThinhLcid2 { get; set; }

    public string RelationshipType { get; set; }

    public bool? ConfirmedByTest { get; set; }

    public string Notes { get; set; }

    public int? Count { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ProfileThinhLc ProfileThinhLcid1Navigation { get; set; }

    public virtual ProfileThinhLc ProfileThinhLcid2Navigation { get; set; }
}
