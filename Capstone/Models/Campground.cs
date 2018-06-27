using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
	public class Campground
	{
		/// <summary>
		/// The Campground Id
		/// </summary>
		public int CampgroundId { get; set; }

		/// <summary>
		/// The Campgound Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The Park id that the campground is located in
		/// </summary>
		public int ParkId { get; set; }

		/// <summary>
		/// The Month the Campground opens
		/// </summary>
		public DateTime OpenMonth { get; set; }

		/// <summary>
		/// The Month the Campground closes
		/// </summary>
		public DateTime CloseMonth { get; set; }

		/// <summary>
		/// The cost per night for any site in the campground
		/// </summary>
		public decimal DailyFee{ get; set; }

		public override string ToString()
		{
			string output = this.Name.PadRight(20);
			output += this.OpenMonth.ToLongDateString().PadRight(10);
			output += this.CloseMonth.ToLongDateString().PadRight(10);
			output += this.DailyFee.ToString("C");
			
			return output;
		}
	}
}
