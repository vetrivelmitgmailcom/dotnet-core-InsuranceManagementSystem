using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceManagementSystemMVC.Models;

public partial class StateMaster
{
    [Key]
    public int StateId { get; set; }

    public string State { get; set; } = null!;

    [ForeignKey("Country")]
    public int CountryId { get; set; }

    public virtual ICollection<CityMaster> CityMasters { get; set; } = new List<CityMaster>();


    public virtual CountryMaster Country { get; set; } = null!;

    public virtual ICollection<PersonalDetail> PersonalDetails { get; set; } = new List<PersonalDetail>();


    public override string ToString()
    {
        return $"State Id: {StateId}\nState: {State}\nCountry Id: {CountryId}";
    }
    
}
