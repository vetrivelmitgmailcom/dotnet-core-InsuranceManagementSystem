using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceManagementSystemMVC.Models;

public partial class MaritalStatusMaster
{
    [Key]
    public int MaritalStatusId { get; set; }

    [Required(ErrorMessage = "Please select the marital status.")]
    [Display(Name = "Marital Status")]
    public string MaritalStatus { get; set; } = null!;

    public virtual ICollection<PersonalDetail> PersonalDetails { get; set; } = new List<PersonalDetail>();


    public override string ToString()
    {
        return $"Marital Status ID: {MaritalStatusId}\nMarital Status: {MaritalStatus}\nPersonal Details Count: {PersonalDetails.Count}";
    }
}
