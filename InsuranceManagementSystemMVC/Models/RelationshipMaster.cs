using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceManagementSystemMVC.Models;

public partial class RelationshipMaster
{
    [Key]
    public int RelationshipId { get; set; }

    public string Relationship { get; set; } = null!;

    public virtual ICollection<NomineeDetail> NomineeDetails { get; set; } = new List<NomineeDetail>();



    public override string ToString()
    {
        return $"Relationship Id: {RelationshipId}\nRelationship: {Relationship}";
    }
}
