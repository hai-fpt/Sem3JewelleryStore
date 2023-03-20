using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class JewelTypeMst
{
    public string Id { get; set; }

    public string JewelleryType { get; set; }

    public string ItemId { get; set; }

    public virtual ItemMst Item { get; set; }

    public virtual ICollection<CartList> Carts { get; } = new List<CartList>();
}