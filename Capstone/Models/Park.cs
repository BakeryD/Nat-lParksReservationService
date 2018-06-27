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

		public override string ToString()
		{
			string output = this.Name + "\n";
			output += "Location:".PadRight(18) + this.Location + "\n";
			output += "Established:".PadRight(18) + this.EstablishDate.ToShortDateString() + "\n";
			output += "Area:".PadRight(18) + this.AreaInSqKm.ToString("N") + " sq km\n";
			output += "Annual Visitors:".PadRight(18) + this.Visitors.ToString("N") + "\n\n";

			string[] descriptionWords = this.Description.Split(' ');
			int lineLength = 85;
			int linePosition = 0;
			foreach (string word in descriptionWords)
			{
				if (linePosition + word.Length > lineLength)
				{
					output += "\n";
					linePosition = 0;
				}
				linePosition += word.Length + 1;
				output += word + " ";
			}

			return output;
		}
	}
}
