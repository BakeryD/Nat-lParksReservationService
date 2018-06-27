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
	}
}
