using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceManagementSystemMVC.Models;

public partial class NomineeDetail
{
    [Key]
    public long NomineeId { get; set; }


    [Required(ErrorMessage = "Please enter the nominee name.")]
    [StringLength(50, ErrorMessage = "The nominee name must not exceed 50 characters.")]
    [RegularExpression("^[A-Z][a-z]*(\\s[A-Z][a-z]*)$", ErrorMessage = "Please enter a valid nominee name.")]
    [Display(Name = "Nominee Name")]
    public string NomineeName { get; set; } = null!;

    [Display(Name = "Policy Id")]
    public long PolicyId { get; set; }


    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
    public DateTime? Dob { get; set; }


    [Display(Name = "Gender Id")]
    public int? GenderId { get; set; }


    [Display(Name = "Mobile")]
    [RegularExpression(@"^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit mobile number.")]
    public long MobileNumber { get; set; }


    [Display(Name = "Aadhar Number")]
    [RegularExpression(@"^[0-9]{4}-[0-9]{4}-[0-9]{4}$", ErrorMessage = "Please enter a valid Aadhar number in the format XXXX-XXXX-XXXX.")]
    public string AadharNumber { get; set; } = null!;



    [Display(Name = "PAN Number")]
    [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Please enter a valid PAN number.")]
    public string PanNumber { get; set; } = null!;


    [Display(Name = "Relationship")]
    public int? RelationshipId { get; set; }



    [ForeignKey("GenderId")]
    public virtual GenderMaster Gender { get; set; } = null!;



    [ForeignKey("PolicyId")]
    public virtual PolicyDetail Policy { get; set; } = null!;


    [ForeignKey("RelationshipId")]
    public virtual RelationshipMaster Relationship { get; set; } = null!;


    public override string ToString()
    {
        return $"Nominee ID: {NomineeId}\nNominee Name: {NomineeName}\nPolicy ID: {PolicyId}\nDate of Birth: {Dob}\nGender ID: {GenderId}\nMobile Number: {MobileNumber}\nAadhar Number: {AadharNumber}\nPAN Number: {PanNumber}\nRelationship ID: {RelationshipId}";
    }
}
