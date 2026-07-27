using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceManagementSystemMVC.Models;

public partial class PaymentTypeMaster
{
    [Key]
    public int PaymentTypeId { get; set; }


    [Display(Name = "Payment Type")]
    public string PaymentType { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();


    public override string ToString()
    {
        return $"Payment Type ID: {PaymentTypeId}\nPayment Type: {PaymentType}\nPayments Count: {Payments.Count}";
    }
}
