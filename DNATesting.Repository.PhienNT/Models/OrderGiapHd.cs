using DNATesting.Repository.PhienNT.Models;
using System;
using System.Collections.Generic;

namespace DNATesting.Repository.PhienNT;

public partial class OrderGiapHd
{
    public int OrderGiapHdid { get; set; }

    public int UserAccountId { get; set; }

    public long TotalAmount { get; set; }

    public string Currency { get; set; }

    public string Status { get; set; }

    public string PaymentStatus { get; set; }

    public string ShippingAddress { get; set; }

    public string RecipientName { get; set; }

    public string RecipientPhone { get; set; }

    public string RecipientEmail { get; set; }

    public string Notes { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public DateTime? CancelledDate { get; set; }

    public string CancelReason { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<TransactionsGiapHd> TransactionsGiapHds { get; set; } = new List<TransactionsGiapHd>();

    public virtual SystemUserAccount UserAccount { get; set; }
}
