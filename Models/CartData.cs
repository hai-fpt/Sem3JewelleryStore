using System;
namespace JewelleryStore.Models
{
	public class CartData
	{
		public List<CartList> Cart { get; set; }
		public UserRegMst UserData { get; set; }
		public decimal TotalCartPrice { get; set; }
	}
}

