using System;
using System.Collections.Generic;
using System.Text;

namespace App3.core.tshirt.Models
{
	public class Output
	{
		public int Id { get; set; }
		public string Order { get; set; }
		public string Observation { get; set; }
		public string DateCreated { get; set; }
		public string Status { get; set; }

		public string Warehouse
		{
			get
			{
				var temp = Details != null && Details.Count > 0 ? Details[0].Warehouse : "";
				return temp;
			}
			set { }
		}

		public List<OutputDetail> Details { get; set; }

		public string _Id
		{
			get
			{
				var temp = string.Concat("00", Id.ToString());
				return temp.Substring(temp.Length - 3);
			}
			set { }
		}
	}
}

