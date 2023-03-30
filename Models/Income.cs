using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class Income
{
    public string CustomerName { get; set; }

    public string CustomerCard { get; set; }

    public string Amount { get; set; }

    public virtual CreditCard CustomerCardNavigation { get; set; }
}
