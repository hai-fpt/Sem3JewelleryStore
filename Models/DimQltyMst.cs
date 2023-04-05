using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class DimQltyMst
{
    public string DimQltyId { get; set; }

    public string DimQlty { get; set; }

    public virtual ICollection<DimMst> DimMsts { get; } = new List<DimMst>();
}
