using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class DimMst
{
    public string StyleCode { get; set; }

    public string DimId { get; set; }

    public string DimQltyId { get; set; }

    public string DimSubTypeId { get; set; }

    public decimal? DimCrt { get; set; }

    public decimal? DimPcs { get; set; }

    public decimal? DimGm { get; set; }

    public decimal? DimSize { get; set; }

    public decimal? DimRate { get; set; }

    public decimal? DimAmt { get; set; }

    public virtual DimInfoMst Dim { get; set; }

    public virtual DimQltyMst DimQlty { get; set; }

    public virtual DimQltySubMst DimSubType { get; set; }

    public virtual ItemMst StyleCodeNavigation { get; set; }
}
