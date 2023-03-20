using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class ProdMst
{
    public string ProdId { get; set; }

    public string ProdType { get; set; }

    public virtual ICollection<ItemMst> ItemMsts { get; } = new List<ItemMst>();
}
