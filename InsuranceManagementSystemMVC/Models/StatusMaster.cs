using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceManagementSystemMVC.Models;

public partial class StatusMaster
{
    [Key]
    public int StatusId { get; set; }

    public string Status { get; set; } = null!;


    public virtual ICollection<PolicyDetail> PolicyDetails { get; set; } = new List<PolicyDetail>();
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();


    public override string ToString()
    {
        return $"Status Id: {StatusId}\nStatus: {Status}";
    }
}
