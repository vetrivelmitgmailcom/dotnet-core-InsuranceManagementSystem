using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceManagementSystemMVC.Models;

public partial class Payment
{

    [Key]
    public long PaymentId { get; set; }


    public long PremiumId { get; set; }


    public int? PaymentTypeId { get; set; }


    [Display(Name = "Premium Amount")]
    [DisplayFormat(DataFormatString = "₹ {0:N2}")]
    public decimal? Amount { get; set; }



    [Display(Name = "Payment Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
    public DateTime? PaymentDate { get; set; }


    [Display(Name = "Payment Type")]
    [ForeignKey("PaymentTypeId")]
    public virtual PaymentTypeMaster PaymentType { get; set; } = null!;


    [ForeignKey("PremiumId")]
    public virtual PolicyValue Premium { get; set; } = null!;


    public override string ToString()
    {
        return $"Payment ID: {PaymentId}\nPremium ID: {PremiumId}\nPayment Type ID: {PaymentTypeId}\nAmount: {Amount}\nPayment Date: {PaymentDate}";
    }
}

