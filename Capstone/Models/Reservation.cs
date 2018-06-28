﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation
    {
		/// <summary>
		/// The reservation Id
		/// </summary>
		public int ReservationId { get; set; }

		/// <summary>
		/// The Camp site id that the Reservation is for
		/// </summary>
		public int SiteId { get; set; }
		
		/// <summary>
		/// The Name of the person who booked the reservation
		/// </summary>
		public string Name { get; set; }
		
		/// <summary>
		/// The Date the reservation starts
		/// </summary>
		public DateTime FromDate { get; set; }

		/// <summary>
		/// The Date the reservation ends
		/// </summary>
		public DateTime ToDate { get; set; }

		/// <summary>
		/// The date the reservation was created
		/// </summary>
		public DateTime CreatedDate { get; set; }

		public override string ToString()
		{
			string output = this.ReservationId.ToString().PadRight(15);
			output += this.SiteId.ToString().PadRight(15);
			output += this.Name.PadRight(25);
			output += this.FromDate.ToString("MM/dd/yyyy").PadRight(15);
			output += this.ToDate.ToString("MM/dd/yyyy").PadRight(15);
			output += this.CreatedDate.ToString("MM/dd/yyyy");

			return output;
		}
	}
}
