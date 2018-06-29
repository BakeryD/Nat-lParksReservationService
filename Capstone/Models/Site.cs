using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
	public class Site
	{
		/// <summary>
		/// The site Id
		/// </summary>
		public int SiteId { get; set; }

		/// <summary>
		/// The Site number
		/// </summary>
		public int Number { get; set; }

		/// <summary>
		/// The campground id that the site is located in
		/// </summary>
		public int CampgroundId { get; set; }

		/// <summary>
		/// The maximum occupancy of the camp site
		/// </summary>
		public int MaxOccupancy { get; set; }

		/// <summary>
		/// Whether the campsite is handicap accesible
		/// </summary>
		public bool HandicapAccessible { get; set; }

		/// <summary>
		/// The maximum RV length the campsite can accomadate
		/// </summary>
		public int MaxRVLength { get; set; }

		/// <summary>
		/// Whether the camp site is equipped with utilities hookup
		/// </summary>
		public bool Utilities { get; set; }

		public override string ToString()
		{
			string output = this.Number.ToString().PadRight(10);
			output += this.MaxOccupancy.ToString().PadRight(12);
			output += (this.HandicapAccessible ? "Yes" : "No").ToString().PadRight(13);
			output += (this.MaxRVLength > 0 ? this.MaxRVLength.ToString() : "N/A").PadRight(15);
			output += (this.Utilities ? "Yes" : "N/A").ToString().PadRight(9);

			return output;
		}
	}
}
