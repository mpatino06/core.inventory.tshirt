using System;
using System.Collections.Generic;
using System.Text;

namespace App3.core.tshirt.Models
{
	public class CountPlanDetailItem
	{
		public int Id { get; set; }
		public int CountPlanId { get; set; }
		public string UserCode { get; set; }
		public string DateCreated { get; set; }
		public int Quantity { get; set; }
		public string ProductCode { get; set; }
		public int Count { get; set; }
	}
}
