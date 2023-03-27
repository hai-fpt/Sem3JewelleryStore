using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class UserRegMst
{
    public string UserId { get; set; }

    public string UserFname { get; set; }

    public string UserLname { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string MobNo { get; set; }

    public string EmailId { get; set; }

    public string Dob { get; set; }

    public string Cdate { get; set; }

    public string Password { get; set; }

    public string Username { get; set; }

    public virtual ICollection<Inquiry> Inquiries { get; } = new List<Inquiry>();
}
