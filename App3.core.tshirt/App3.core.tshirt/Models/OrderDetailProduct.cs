using System;
using System.Collections.Generic;
using System.Text;

namespace App3.core.tshirt.Models
{
	public class OrderDetailProduct
	{
		public int Id { get; set; }
		public string OrderCode { get; set; }
		public string UserCode { get; set; }
		public string DateCreated { get; set; }
		public int Quantity { get; set; }
		public string ProductCode { get; set; }
		public string ProviderCode { get; set; }
		public string FullDescription
		{
			get { return OrderCode + " " + UserCode + " " + DateCreated; }

		}
	}
}