using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InsuranceManagementSystemMVC.Models;

public partial class Admin
{
    [Key]
    public int AdminId { get; set; }

    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Please enter a password.")]
    [DataType(DataType.Password)]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "The password must be between 8 and 20 characters.")]
    public string? Password { get; set; }


    public override string ToString()
    {
        return $"Admin ID: {AdminId}\nEmail: {Email}\nPassword: {Password}";
    }
}
