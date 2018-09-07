using System;
using System.Collections.Generic;
using System.Text;

namespace App3.core.tshirt.Models
{
	public class ProductTransfer
	{
		public int Id { get; set; }
		public string warehouseOrigin { get; set; }
		public string warehouseDestiny { get; set; }
		public List<ProductToTransfer> products { get; set; }
		public string dateCreated { get; set; }
		public string status { get; set; }
		public string observation { get; set; }
	}
}
