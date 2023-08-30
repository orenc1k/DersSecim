using System;
using System.ComponentModel.DataAnnotations;

namespace ODTUDersSecim.Models
{
	public class Departments
	{

		[Key]
		public int DeptCode { get; set; }
		public string? DeptShortName { get; set; }
		public string? DeptFullName { get; set;}
	}
}

