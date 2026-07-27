using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceManagementSystemMVC.Models;

public partial class Customer
{
    [Key]
    [Display(Name = "Customer Id")]
    public long CustomerId { get; set; }


    [Required(ErrorMessage = "Please enter the first name.")]
    [StringLength(50, ErrorMessage = "The first name must not exceed 50 characters.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;


    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "Please enter the last name.")]
    [StringLength(50, ErrorMessage = "The last name must not exceed 50 characters.")]
    public string LastName { get; set; } = null!;


    [NotMapped]                                                                                          //ipdi kudukalena error varum,because Insurancecontext la illa,(Athukku pathila InsuranceContext laium Age pathi add panna apium error varum,because Dtabase la Age column illai)
    [Range(18, 120, ErrorMessage = "The age must be between 18 and 120.")]
    public int Age { get; set; }


    [Range(0, 1, ErrorMessage = "Status must be either 0 or 1.")]
    public int StatusId { get; set; }


    public virtual ICollection<PersonalDetail> PersonalDetails { get; set; } = new List<PersonalDetail>();

    public virtual ICollection<PolicyDetail> PolicyDetails { get; set; } = new List<PolicyDetail>();

    [ForeignKey("StatusId")]
    public virtual StatusMaster Status { get; set; } = null!;

    public override string ToString()
    {
        return $"Customer ID: {CustomerId}\nFirst Name: {FirstName}\nLast Name: {LastName}\nPersonal Details Count: {PersonalDetails.Count}\nPolicy Details Count: {PolicyDetails.Count}";
    }
}
