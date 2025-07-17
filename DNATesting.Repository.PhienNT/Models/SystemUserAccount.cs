using System;
using System.Collections.Generic;

namespace DNATesting.Repository.PhienNT.Models;

public partial class SystemUserAccount
{
    public int UserAccountId { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string EmployeeCode { get; set; }

    public int RoleId { get; set; }

    public string? RequestCode { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ApplicationCode { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AppointmentsTienDm> AppointmentsTienDms { get; set; } = new List<AppointmentsTienDm>();

    public virtual ICollection<BlogsHuyLhg> BlogsHuyLhgs { get; set; } = new List<BlogsHuyLhg>();

    public virtual ICollection<OrderGiapHd> OrderGiapHds { get; set; } = new List<OrderGiapHd>();

    public virtual ICollection<ProfileThinhLc> ProfileThinhLcs { get; set; } = new List<ProfileThinhLc>();

    public virtual ICollection<ServicesNhanVt> ServicesNhanVts { get; set; } = new List<ServicesNhanVt>();
}
