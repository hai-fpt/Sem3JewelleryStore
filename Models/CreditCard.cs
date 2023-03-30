using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class CreditCard
{
    public string CardNumber { get; set; }

    public string CardName { get; set; }

    public string CardCvv { get; set; }

    public string CardExpiration { get; set; }

    public virtual ICollection<Income> Incomes { get; } = new List<Income>();
}
