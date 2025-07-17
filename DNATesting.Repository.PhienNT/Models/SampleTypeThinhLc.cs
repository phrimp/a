using System;
using System.Collections.Generic;

namespace DNATesting.Repository.PhienNT;

public partial class SampleTypeThinhLc
{
    public int SampleTypeThinhLcid { get; set; }

    public string TypeName { get; set; }

    public string Description { get; set; }

    public bool IsActive { get; set; }

    public int? Count { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<SampleThinhLc> SampleThinhLcs { get; set; } = new List<SampleThinhLc>();
}
