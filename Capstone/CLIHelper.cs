using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
	public class CLIHelper
	{
		/// <summary>
		/// Gets a datetime from the user after a specified date
		/// </summary>
		/// <param name="laterThanThis">A date the provided input should be after</param>
		/// <returns>A validated datetime</returns>
		public static DateTime GetDateTime(DateTime laterThanThis)
		{
			// Initialize output
			DateTime output = DateTime.MinValue;

			// Get input from the user
			string input = Console.ReadLine();

			// Check that the output is later than the specified date
			while (output.Date < laterThanThis.Date)
			{
				// If the input is a valid datetime, pasre it to output
				if (DateTime.TryParse(input, out DateTime result))
				{
					output = DateTime.Parse(input);
					// If a time of day was entered, get rid of it
					output.Subtract(output.TimeOfDay);
				}
				// If check fails, reprompt the user for input
				if (output.Date < laterThanThis.Date)
				{
					Console.Write("Please enter a valid date:");
					input = Console.ReadLine();
				}
			}
			//Return the validated output
			return output;
		}

		/// <summary>
		/// Gets the total cost of the trip
		/// </summary>
		/// <param name="start">The start date of the trip</param>
		/// <param name="end">The end date of the trip</param>
		/// <param name="dailyFee">The fee per day for the site</param>
		/// <returns></returns>
		public static decimal GetTripTotal(DateTime start, DateTime end, decimal dailyFee)
		{
			// Calculate the length of the stay
			int lengthOfStay = (end - start).Days + 1;
			// Return the length of the stay times the daily fee
			return lengthOfStay * dailyFee;
		}

		/// <summary>
		/// Gets an integer from the user within a specified range
		/// </summary>
		/// <param name="low">The lowest possible input (inclusive)</param>
		/// <param name="high">The highest possible input (inclusive)</param>
		/// <returns>A number input from the User</returns>
		public static int GetAnInteger(int low, int high)
		{
			// Take input from the user
			string input = Console.ReadLine();
			int output = int.MinValue;

			// Check if the input is an int and within range. 
			// Keep asking until the user puts in an int within specified range
			while (output < low || output > high)
			{
				// If possible, parse the input into an int
				if (int.TryParse(input, out output))
				{
					output = int.Parse(input);
				}
				// Otherwise
				if (output < low || output > high)
				{
					Console.Write($"Please enter a number between {low} and {high}: ");
					input = Console.ReadLine();
				}
			}
			// Return the number
			return output;
		}

        public static string GetString()
        {
            string input = Console.ReadLine();

            while (input == "")
            {
                Console.Write("Please enter a valid input: ");
                input = Console.ReadLine();
            }
            return input;
        }
    }
}
