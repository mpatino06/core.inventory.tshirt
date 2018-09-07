using System;
using System.Collections.Generic;
using System.Text;

namespace App3.core.tshirt.Models
{
	public class ProductToTransfer
	{
		public long quantity { get; set; }
		public long quantityAvailable { get; set; }
		public string productCode { get; set; }
		public string productDescription { get; set; }
	}
}
