using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceManagementSystemMVC.Models;

public partial class PolicyDetail
{
    [Key]
    [Display(Name = "Policy Number")]
    public long PolicyId { get; set; }


    [Display(Name = "Customer Id")]
    public long CustomerId { get; set; }


    [Display(Name = "Insurance Name")]
    public int InsuranceId { get; set; }



    [Display(Name = "Date of Issue")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
    public DateTime DateOfIssue { get; set; }


    [Display(Name = "Expiry Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
    public DateTime? DateOfExpire { get; set; }


    [Range(0, 1, ErrorMessage = "Status must be either 0 or 1.")]
    public int StatusId { get; set; }


    [ForeignKey("CustomerId")]
    public virtual Customer Customer { get; set; } = null!;


    [ForeignKey("InsuranceId")]
    public virtual InsuranceTypeMaster Insurance { get; set; } = null!;

    public virtual ICollection<NomineeDetail> NomineeDetails { get; set; } = new List<NomineeDetail>();


    [ForeignKey("PolicyId")]
    public virtual PolicyValue PolicyValue { get; set; } = null!;


    [ForeignKey("StatusId")]
    public virtual StatusMaster Status { get; set; } = null!;



    public override string ToString()
    {
        return $"Policy Number: {PolicyId}\nCustomer ID: {CustomerId}\nInsurance ID: {InsuranceId}\nDate of Issue: {DateOfIssue}\nDate of Expire: {DateOfExpire}\nStatus ID: {StatusId}";
    }
}
