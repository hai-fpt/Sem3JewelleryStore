using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class CatMst
{
    public string CatId { get; set; }

    public string CatName { get; set; }

    public virtual ICollection<ItemMst> ItemMsts { get; } = new List<ItemMst>();
}
