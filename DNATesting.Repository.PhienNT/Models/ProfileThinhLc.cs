using DNATesting.Repository.PhienNT.Models;
using System;
using System.Collections.Generic;

namespace DNATesting.Repository.PhienNT;

public partial class ProfileThinhLc
{
    public int ProfileThinhLcid { get; set; }

    public int? UserAccountId { get; set; }

    public string FullName { get; set; }

    public string Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string NationalId { get; set; }

    public bool IsDeceased { get; set; }

    public string Notes { get; set; }

    public int? Count { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<AlleleResultsPhienNt> AlleleResultsPhienNts { get; set; } = new List<AlleleResultsPhienNt>();

    public virtual ICollection<ProfileRelationshipThinhLc> ProfileRelationshipThinhLcProfileThinhLcid1Navigations { get; set; } = new List<ProfileRelationshipThinhLc>();

    public virtual ICollection<ProfileRelationshipThinhLc> ProfileRelationshipThinhLcProfileThinhLcid2Navigations { get; set; } = new List<ProfileRelationshipThinhLc>();

    public virtual ICollection<SampleThinhLc> SampleThinhLcs { get; set; } = new List<SampleThinhLc>();

    public virtual SystemUserAccount UserAccount { get; set; }
}
