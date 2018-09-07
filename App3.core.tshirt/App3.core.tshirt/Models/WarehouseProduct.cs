using System;
using System.Collections.Generic;
using System.Text;

namespace App3.core.tshirt.Models
{
	public class WarehouseProduct
	{
		public int Id { get; set; }
		public string WarehouseCode { get; set; }
		public string ProductCode { get; set; }
		public long Quantity { get; set; }
		public Warehouse Warehouse { get; set; }
		public Product Product { get; set; }
	}
}