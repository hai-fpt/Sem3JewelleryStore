﻿using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class DimQltySubMst
{
    public string DimSubTypeId { get; set; }

    public string DimQlty { get; set; }

    public virtual ICollection<DimMst> DimMsts { get; } = new List<DimMst>();
}
