using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceManagementSystemMVC.Models;

public partial class PolicyValue
{
    [Key]
    [Display(Name = "Premium Id")]
    public long PremiumId { get; set; }


    [Display(Name = "Policy Id")]
    public long PolicyId { get; set; }


    [Display(Name = "No of period")]
    [DisplayFormat(DataFormatString = "{0}")]
    //[DisplayFormat(DataFormatString = "{0} Year(s)")]
    public int AmountOfPeriod { get; set; }


    [Display(Name = "Insured Declared Value")]
    [DisplayFormat(DataFormatString = "₹ {0:0}")]
    //[DataType(DataType.Currency)]
    public decimal? InsuredDeclaredValue { get; set; }


    [Display(Name = "Premium Amount")]
    [DisplayFormat(DataFormatString = "₹ {0:N2}")]
    public decimal? PremiumToBePaid { get; set; }


    [Display(Name = "Mode of Premium")]
    public int? ModeOfPremiumId { get; set; }



    [Display(Name = "Mode of Premium")]
    [ForeignKey("ModeOfPremiumId")]
    public virtual ModeOfPremiumMaster ModeOfPremium { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();


    [ForeignKey("PolicyId")]
    public virtual PolicyDetail Policy { get; set; } = null!;


    public override string ToString()
    {
        return $"Premium Id: {PremiumId}\nPolicy Id: {PolicyId}\nNo of period: {AmountOfPeriod} Year(s)\nInsured Declared Value: ₹ {InsuredDeclaredValue}\nPremium Amount: ₹ {PremiumToBePaid}\nMode of Premium: {ModeOfPremiumId}";
    }
}
