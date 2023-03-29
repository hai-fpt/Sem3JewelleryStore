using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class CartList
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string Quantity { get; set; }

    public decimal Price { get; set; }
}
