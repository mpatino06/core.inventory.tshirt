﻿using System;
using System.Collections.Generic;
using System.Text;

namespace App3.core.tshirt.Models
{
	public class User
	{
		public int Id { get; set; }

		public string Code { get; set; }

		public string Name { get; set; }

		public int RolId { get; set; }

		public string Observation { get; set; }

		public string DateCreated { get; set; }

		public string Value1 { get; set; }

		public string Value2 { get; set; }

		public string Value3 { get; set; }

		public string Value4 { get; set; }

		public string Value5 { get; set; }

		public string Password { get; set; }
		public int IsActive { get; set; }
	}
}