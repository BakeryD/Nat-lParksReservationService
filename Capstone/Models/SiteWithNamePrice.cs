using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
	public class SiteWithNamePrice : Site
	{
		public string Name { get; set; }
		public decimal DailyFee { get; set; }

		public override string ToString()
		{
			return this.Name.PadRight(15) + base.ToString();
		}
	}
}
