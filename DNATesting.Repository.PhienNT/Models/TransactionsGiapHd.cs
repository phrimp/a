using System;
using System.Collections.Generic;

namespace DNATesting.Repository.PhienNT.Models;

public partial class TransactionsGiapHd
{
    public int TransactionsGiapHdid { get; set; }

    public int OrderGiapHdid { get; set; }

    public long Amount { get; set; }

    public string Currency { get; set; }

    public string PaymentMethod { get; set; }

    public string PaymentGateway { get; set; }

    public bool? IsRefund { get; set; }

    public string RefundReason { get; set; }

    public string TransactionReference { get; set; }

    public string Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual OrderGiapHd OrderGiapHd { get; set; }
}
