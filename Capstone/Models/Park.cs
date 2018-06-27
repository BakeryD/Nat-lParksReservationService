using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Park
    {
		/// <summary>
		/// The Park Id
		/// </summary>
		public int ParkId { get; set; }

		/// <summary>
		/// The Park Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The Park's location
		/// </summary>
		public string Location { get; set; }

		/// <summary>
		/// The Date the Park was established
		/// </summary>
		public DateTime EstablishDate { get; set; }

		/// <summary>
		/// The area of the park in square km
		/// </summary>
		public int AreaInSqKm { get; set; }

		/// <summary>
		/// The annual number of visitors to the park
		/// </summary>
		public int Visitors { get; set; }

		/// <summary>
		/// The park's description
		/// </summary>
		public string Description { get; set; }
	}
}
