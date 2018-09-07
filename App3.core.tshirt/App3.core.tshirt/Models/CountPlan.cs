using System;
using System.Collections.Generic;
using System.Text;

namespace App3.core.tshirt.Models
{
	public class CountPlan
	{

		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string Status { get; set; }

		public string DateCreated { get; set; }

		public string Warehouse { get; set; }

		public string Value2 { get; set; }

		public string Value3 { get; set; }

		public string Value4 { get; set; }

		public string Value5 { get; set; }

		public string UserUpdated { get; set; }

		public string DateUpdated { get; set; }

		public List<CountPlanDetail> Details { get; set; }
	}
}
