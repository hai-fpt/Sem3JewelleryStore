using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class CertifyMst
{
    public string CertifyId { get; set; }

    public string CertifyType { get; set; }

    public virtual ICollection<ItemMst> ItemMsts { get; } = new List<ItemMst>();
}
