using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class Inquiry
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string City { get; set; }

    public string Contact { get; set; }

    public string EmailId { get; set; }

    public string Comment { get; set; }

    public DateOnly Cdate { get; set; }

    public string UserId { get; set; }

    public virtual UserRegMst User { get; set; }
}
