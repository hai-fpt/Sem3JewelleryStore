using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class DimInfoMst
{
    public string DimId { get; set; }

    public string DimType { get; set; }

    public string DimSubType { get; set; }

    public string DimCrt { get; set; }

    public string DimPrice { get; set; }

    public string DimImg { get; set; }

    public virtual DimMst DimMst { get; set; }
}
