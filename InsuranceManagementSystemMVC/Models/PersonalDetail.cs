using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Resources;

namespace InsuranceManagementSystemMVC.Models;

public partial class PersonalDetail
{
    [Key]
    [Display(Name = "Personal Id")]                               //[DisplayName("Personal Id")]
    public long PersonalId { get; set; }



    [Display(Name = "Customer Id")]

    public long CustomerId { get; set; }



    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
    //[DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Dob { get; set; }



    [Display(Name = "Gender Id")]
    public int? GenderId { get; set; }



    [Display(Name = "Marital Status")]
    public int? MaritalStatusId { get; set; }



    [Display(Name = "Mobile")]
    //[Editable(false)]
    [DataType(DataType.PhoneNumber)]
    public long MobileNumber { get; set; }


    //[EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;



    [Display(Name = "Aadhar Number")]
    [DisplayFormat(DataFormatString = "{0:#### #### ####}")]
    public string AadharNumber { get; set; } = null!;


    [Display(Name = "PAN Number")]
    public string PanNumber { get; set; } = null!;


    [Display(Name = "Address")]
    public string Street { get; set; } = null!;


    [Display(Name = "City")]
    public int CityId { get; set; }


    [Display(Name = "State")]
    public int StateId { get; set; }


    [Display(Name = "Country")]
    public int CountryId { get; set; }


    [Display(Name = "Pin Code")]
    public long PostalCode { get; set; }




    [ForeignKey("CityId")]
    [Display(Name = "City")]
    public virtual CityMaster City { get; set; } = null!;



    [ForeignKey("CountryId")]
    [Display(Name = "Country")]
    public virtual CountryMaster Country { get; set; } = null!;




    [ForeignKey("CustomerId")]
    [Display(Name = "Customer")]    
    public virtual Customer Customer { get; set; } = null!;



    [ForeignKey("GenderId")]
    [Display(Name = "Gender")]
    public virtual GenderMaster Gender { get; set; } = null!;



    [ForeignKey("MaritalStatusId")]
    [Display(Name = "Marital Status")]
    public virtual MaritalStatusMaster MaritalStatus { get; set; } = null!;


    [ForeignKey("StateId")]
    [Display(Name = "State")]
    public virtual StateMaster State { get; set; } = null!;


    public override string ToString()
    {
        return $"Personal ID: {PersonalId}\nCustomer ID: {CustomerId}\nDate of Birth: {Dob}\nGender ID: {GenderId}\nMarital Status ID: {MaritalStatusId}\nMobile Number: {MobileNumber}\nEmail: {Email}\nAadhar Number: {AadharNumber}\nPAN Number: {PanNumber}\nAddress: {Street}\nCity ID: {CityId}\nState ID: {StateId}\nCountry ID: {CountryId}\nPostal Code: {PostalCode}";
    }
}
