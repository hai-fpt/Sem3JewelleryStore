using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class BrandMst
{
    public string BrandId { get; set; }

    public string BrandType { get; set; }

    public virtual ICollection<ItemMst> ItemMsts { get; } = new List<ItemMst>();
}
