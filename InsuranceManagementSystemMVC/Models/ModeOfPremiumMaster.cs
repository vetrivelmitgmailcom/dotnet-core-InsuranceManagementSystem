using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceManagementSystemMVC.Models;

public partial class ModeOfPremiumMaster
{
    [Key]
    public int ModeOfPremiumId { get; set; }


    [Required(ErrorMessage = "Please select the mode of premium.")]
    [Display(Name = "Mode of premium")]
    public string ModeOfPremium { get; set; } = null!;

    public virtual ICollection<PolicyValue> PolicyValues { get; set; } = new List<PolicyValue>();


    public override string ToString()
    {
        return $"Mode of Premium ID: {ModeOfPremiumId}\nMode of Premium: {ModeOfPremium}\nPolicy Values Count: {PolicyValues.Count}";
    }
}
