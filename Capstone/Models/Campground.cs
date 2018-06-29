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
		public int OpenMonth { get; set; }

		/// <summary>
		/// The Month the Campground closes
		/// </summary>
		public int CloseMonth { get; set; }

		/// <summary>
		/// The cost per night for any site in the campground
		/// </summary>
		public decimal DailyFee { get; set; }
        
		public override string ToString()
		{
			string output = this.Name.PadRight(32);
			output += NumberToMonthName(this.OpenMonth).PadRight(10);
			output += NumberToMonthName(this.CloseMonth).PadRight(10);
			output += this.DailyFee.ToString("C");

			return output;
		}

		private string NumberToMonthName(int number)
		{
			Dictionary<int, string> monthNames = new Dictionary<int, string>()
			{
				{ 1,"January" },
				{ 2,"Febuary" },
				{ 3,"March" },
				{ 4,"April" },
				{ 5,"May" },
				{ 6,"June" },
				{ 7,"July" },
				{ 8,"August" },
				{ 9,"September" },
				{ 10,"October" },
				{ 11,"November" },
				{ 12,"December" }
			};

			return monthNames[number];
		}

	}
}
