using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceManagementSystemMVC.Models;

public partial class InsuranceTypeMaster
{
    [Key]
    public int InsuranceId { get; set; }

    [Required(ErrorMessage = "Please select the insurance name.")]
    [Display(Name = "Insurance Name")]
    public string InsuranceType { get; set; } = null!;

    public virtual ICollection<PolicyDetail> PolicyDetails { get; set; } = new List<PolicyDetail>();

    public override string ToString()
    {
        return $"Insurance ID: {InsuranceId}\nInsurance Name: {InsuranceType}\nPolicy Details Count: {PolicyDetails.Count}";
    }
}
