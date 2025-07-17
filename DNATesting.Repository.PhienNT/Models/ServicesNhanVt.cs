using System;
using System.Collections.Generic;

namespace DNATesting.Repository.PhienNT.Models;

public partial class ServicesNhanVt
{
    public int ServicesNhanVtid { get; set; }

    public int ServiceCategoryNhanVtid { get; set; }

    public string ServiceName { get; set; }

    public string Description { get; set; }

    public decimal? Price { get; set; }

    public int? Duration { get; set; }

    public bool? IsSelfSampleAllowed { get; set; }

    public bool? IsHomeVisitAllowed { get; set; }

    public bool? IsClinicVisitAllowed { get; set; }

    public int? ProcessingTime { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsActive { get; set; }

    public int UserAccountId { get; set; }

    public virtual ICollection<AppointmentsTienDm> AppointmentsTienDms { get; set; } = new List<AppointmentsTienDm>();

    public virtual ServiceCategoriesNhanVt ServiceCategoryNhanVt { get; set; }

    public virtual SystemUserAccount UserAccount { get; set; }
}
