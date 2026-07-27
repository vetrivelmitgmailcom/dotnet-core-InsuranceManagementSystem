using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceManagementSystemMVC.Models;

public partial class GenderMaster
{
    [Key]
    public int GenderId { get; set; }

    [Required(ErrorMessage = "Please select the gender.")]
    public string Gender { get; set; } = null!;

    public virtual ICollection<NomineeDetail> NomineeDetails { get; set; } = new List<NomineeDetail>();

    public virtual ICollection<PersonalDetail> PersonalDetails { get; set; } = new List<PersonalDetail>();


    public override string ToString()
    {
        return $"Gender ID: {GenderId}\nGender: {Gender}\nNominee Details Count: {NomineeDetails.Count}\nPersonal Details Count: {PersonalDetails.Count}";
    }
}
