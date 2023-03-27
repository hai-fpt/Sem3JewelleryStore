using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class GoldKrtMst
{
    public string GoldTypeId { get; set; }

    public string GoldCrt { get; set; }

    public virtual ICollection<ItemMst> ItemMsts { get; } = new List<ItemMst>();
}
