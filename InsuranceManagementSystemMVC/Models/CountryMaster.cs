using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceManagementSystemMVC.Models;

public partial class CountryMaster
{
    [Key]
    public int CountryId { get; set; }

    [Required(ErrorMessage = "Please select the country name.")]
    public string Country { get; set; } = null!;


    public virtual ICollection<PersonalDetail> PersonalDetails { get; set; } = new List<PersonalDetail>();

    public virtual ICollection<StateMaster> StateMasters { get; set; } = new List<StateMaster>();

    public override string ToString()
    {
        return $"Country ID: {CountryId}\nCountry: {Country}\nNumber of Personal Details: {PersonalDetails.Count}\nNumber of State Masters: {StateMasters.Count}";
    }
}
