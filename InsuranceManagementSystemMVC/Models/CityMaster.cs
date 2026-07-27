using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceManagementSystemMVC.Models;

public partial class CityMaster
{
    [Key]
    public int CityId { get; set; }


    [Required(ErrorMessage = "Please select city name.")]
    public string City { get; set; } = null!;


    public int StateId { get; set; }

    public virtual ICollection<PersonalDetail> PersonalDetails { get; set; } = new List<PersonalDetail>();



    [ForeignKey("StateId")]
    public virtual StateMaster State { get; set; } = null!;


    public override string ToString()
    {
        return $"City ID: {CityId}\nCity: {City}\nState ID: {StateId}\nState: {State}";
    }
}
