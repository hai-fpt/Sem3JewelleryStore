using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class CartList
{
    public string Id { get; set; }

    public string ProductName { get; set; }

    public decimal Mrp { get; set; }

    public virtual ICollection<JewelTypeMst> Jewels { get; } = new List<JewelTypeMst>();
}
