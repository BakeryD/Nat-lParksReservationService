using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
	public class CLI
	{
		private const string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";
		private static ParkDAL parkDAL = new ParkDAL(ConnectionString);
		private static CampgroundDAL campDAL = new CampgroundDAL(ConnectionString);
		private static SiteDAL siteDAL = new SiteDAL(ConnectionString);
		private static ReservationDAL resDAL = new ReservationDAL(ConnectionString);

		/// <summary>
		/// Runs the program
		/// </summary>
		public void Run()
		{
			PrintHeader();
			MainMenu();
		}

		/// <summary>
		/// Prints a splash screen greeting
		/// </summary>
		private void PrintHeader()
		{
			Console.Clear();
			Console.WriteLine("  Welcome to the");
			Console.WriteLine();
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(@"            ,-.----.                                 ");
			Console.WriteLine(@"            \    /  \                           ,-.  ");
			Console.WriteLine(@"            |   :    \                      ,--/ /|  ");
			Console.WriteLine(@"            |   |  .\ :            __  ,-.,--. :/ |  ");
			Console.WriteLine(@"            .   :  |: |          ,' ,'/ /|:  : ' /   ");
			Console.WriteLine(@"            |   |   \ : ,--.--.  '  | |' ||  '  /    ");
			Console.WriteLine(@"            |   : .   //       \ |  |   ,''  |  :    ");
			Console.WriteLine(@"            ;   | |`-'.--.  .-. |'  :  /  |  |   \   ");
			Console.WriteLine(@"            |   | ;    \__\/: . .|  | '   '  : |. \  ");
			Console.WriteLine(@"            :   ' |    ,' .--.; |;  : |   |  | ' \ \ ");
			Console.WriteLine(@"            :   : :   /  /  ,.  ||  , ;   '  : |--'  ");
			Console.WriteLine(@"            |   | :  ;  :   .'   \---'    ;  |,'     ");
			Console.WriteLine(@"            `---'.|  |  ,     .-./        '--'       ");
			Console.WriteLine(@"              `---`   `--`---'                       ");
			Console.WriteLine();
			Console.WriteLine();
			Console.Write("               ");
			Console.ForegroundColor = ConsoleColor.White;
			string line = "Finding Shrubbery Galore.....";
			foreach (char letter in line)
			{
				Console.Write(letter);
				System.Threading.Thread.Sleep(75);
			}
			System.Threading.Thread.Sleep(100);

			Console.Clear();
		}

		/// <summary>
		/// Handles the Main Menu
		/// </summary>
		private void MainMenu()
		{

			while (true)
			{
				// Print the main menu and take user input
				int input = PrintMainMenu();

				// Retrieve the list of parks
				List<Park> parks = parkDAL.GetParks();

				// If the user chose to quit, close the program
				if (input == parks.Count + 1)
				{
					Console.Clear();
					ResizeAndExitWindow();
					return;
				}
				// Otherwise, enter the Parks menu usering the user's designated park
				else
				{
					ParkMenu(parks[input - 1]);
				}
			}
		}

		/// <summary>
		/// Displays the main Menu
		/// </summary>
		/// <returns>The user's choice</returns>
		private static int PrintMainMenu()
		{
			Console.Clear();
			Console.WriteLine("Pick a Park to View");
			Console.WriteLine();

			//Get a list of park objects from ParkDAL
			List<Park> parks = parkDAL.GetParks();

			//Display each park name, and the quit option
			for (int i = 1; i <= parks.Count; i++)
			{
				Console.WriteLine($" {i}) {parks[i - 1].Name}");
			}
			Console.WriteLine($" {parks.Count + 1}) Quit");
			Console.WriteLine();
			Console.Write(" > ");

			// Return the user's input
			return CLIHelper.GetAnInteger(1, parks.Count + 1);
		}

		/// <summary>
		/// Handles the park Menu
		/// </summary>
		/// <param name="currentPark">The park the user chose to look at</param>
		private void ParkMenu(Park currentPark)
		{
			Console.Clear();
			int input;
			do
			{
				// Get user input
				input = PrintParkMenu(currentPark);

				// Parse user input
				switch (input)
				{
					case 1:		// Serach a specific campground in the park
						CampgroundMenu(currentPark);
						break;
					case 2:		// Search Parkwide for a reservation
						SearchForReservation(currentPark, true);
						break;
				}
			} while (input != 3);	// Continue until the user enters the quit command
		}

		/// <summary>
		/// Prints the Park menu
		/// </summary>
		/// <param name="currentPark">The park to display</param>
		/// <returns>The user's input</returns>
		private static int PrintParkMenu(Park currentPark)
		{
			Console.Clear();
			Console.WriteLine(currentPark.ToString());
			Console.WriteLine();
			Console.WriteLine("Choose an option");
			Console.WriteLine(" 1) View Campgrounds");           
			Console.WriteLine(" 2) Search for Reservation");     
			Console.WriteLine(" 3) Return to Previous Screen");  
			Console.WriteLine();
			Console.Write(" > ");
			
			// Return user input
			return CLIHelper.GetAnInteger(1, 3);
		}

		/// <summary>
		/// Handles the campground menu
		/// </summary>
		/// <param name="currentPark">The park to search in</param>
		private void CampgroundMenu(Park currentPark)
		{
			int input;
			do
			{
				// Get user input
				input = PrintCampgroundMenu(currentPark);
				switch (input)
				{
					case 1:		// Search for a reservation
						SearchForReservation(currentPark, false);
						break;
				}
			} while (input != 2);   // Continue until the user enters the quit command
		}

		/// <summary>
		/// Prints the campground menu
		/// </summary>
		/// <param name="currentPark">The park to look in</param>
		/// <returns>The user's input</returns>
		private int PrintCampgroundMenu(Park currentPark)
		{
			Console.Clear();
			// Print a list  Campgrounds
			PrintCampgroundList(currentPark);
			Console.WriteLine("Choose an option");
			Console.WriteLine("   1) Search for Available Reservation");
			Console.WriteLine("   2) Return to Previous Screen");
			Console.WriteLine();
			Console.Write("  >");

			// Return the user input
			return CLIHelper.GetAnInteger(1, 2);
		}

		private void SearchForReservation(Park currentPark, bool fromWholePark)
		{
			Console.Clear();
			List<Campground> campgrounds = campDAL.GetCampgrounds(currentPark);
			List<Site> sites = new List<Site>();
			DateTime startDate = DateTime.MinValue;
			DateTime endDate = DateTime.MinValue;
			int input = -1;
			bool continueSearching = true;

			do
			{
				if (!fromWholePark)
				{
					campgrounds = campDAL.GetCampgrounds(currentPark);
					Console.WriteLine($"Campgrounds in {currentPark.Name}");
					PrintCampgroundList(currentPark);

					Console.Write("Which Campground (enter 0 to cancel)? ");
					input = CLIHelper.GetAnInteger(0, campgrounds.Count);
					if (input != 0)
					{
						campgrounds = new List<Campground>() { campgrounds[input - 1] };
					}
				}
				if (input != 0)
				{
					int occupants = 1;
					bool isAccessible = false;
					int RVLength = 0;
					bool hasUtilities = false;

					Console.Write(">Enter a Start Date for Reservation:  ");
					startDate = CLIHelper.GetDateTime(DateTime.Now.Date);
					Console.Write(">Enter a Departure Date for Reservation:  ");
					endDate = CLIHelper.GetDateTime(startDate);

					Console.Write("Would you like to preform an Advanced Search? (Y/N):  ");
					bool isAdvancedSearch = CLIHelper.GetBoolean();
					if (isAdvancedSearch)
					{
						Console.WriteLine();
						Console.Write("How many occupants:  ");
						occupants = CLIHelper.GetAnInteger(1, 55);
						Console.Write("Do you need Wheelchair Accessiblity? (Y/N):  ");
						isAccessible = CLIHelper.GetBoolean();
						Console.Write("How long is your RV? (Enter 0 if not applicable):  ");
						RVLength = CLIHelper.GetAnInteger(0, 35);
						Console.Write("Utilities Required? (Y/N):  ");
						hasUtilities = CLIHelper.GetBoolean();
					}
					sites = PrintSiteList(startDate, endDate, campgrounds, occupants, isAccessible, RVLength, hasUtilities);

					if (sites.Count == 0)
					{
						Console.Clear();
						Console.WriteLine("No Available Sites per your specifications.");
						Console.Write("Would you like to try again? (Y/N): ");
						continueSearching = CLIHelper.GetBoolean();
						Console.WriteLine();
					}
				}
			} while (sites.Count == 0 && input != 0 && continueSearching);

			if (sites.Count != 0)
			{
				BookAReservation(sites, startDate, endDate);
			}
		}

		private void PrintCampgroundList(Park currentPark)
		{
			List<Campground> campgrounds = campDAL.GetCampgrounds(currentPark);
			Console.WriteLine("     Name                            Open      Close     Daily Fee");
			for (int i = 1; i <= campgrounds.Count; i++)
			{
				Console.WriteLine($"#{i}   " + campgrounds[i - 1].ToString());
			}
			Console.WriteLine();
		}

		private void BookAReservation(List<Site> sites, DateTime startDate, DateTime endDate)
		{

			//else, as user to choose a site
			Console.WriteLine("Which site should be reserved (enter 0 to cancel) ");
			int input = CLIHelper.GetAnInteger(0, sites.Count);

			if (input != 0)
			{
				Console.WriteLine("What Name should the reservation be made under? ");
				string name = CLIHelper.GetString();

				Reservation reservation = new Reservation()
				{
					SiteId = sites[input - 1].SiteId,
					Name = name,
					FromDate = startDate,
					ToDate = endDate
				};

				int resId = resDAL.MakeReservation(reservation);

				Console.WriteLine();
				Console.WriteLine($"The Reservation has been made and your confirmation Id is {resId}");
				Console.WriteLine("Press enter to continue");
				Console.ReadKey(true);
				Console.Clear();
			}
		}

		private List<Site> PrintSiteList(DateTime start, DateTime end, List<Campground> campgrounds, int occupants, bool isAccessible, int RVLength, bool hasUtilities)
		{
			List<string> screenOutput = new List<string>();
			List<Site> allSites = new List<Site>();
			int menuNumber = 1;
			string header = "Site No.  Max Occup.  Accessible?  Max RV Length  Utility  Cost";
			if (campgrounds.Count > 1)
			{
				header = "Campground".PadRight(32) + header;
			}
			header = "     " + header;
			screenOutput.Add(header);

			foreach (Campground campground in campgrounds)
			{
				List<Site> sites = siteDAL.FindAvailableSitesAdvanced(start, end, campground, occupants, isAccessible, RVLength, hasUtilities);
				foreach (Site site in sites)
				{
					string line = site.ToString() + CLIHelper.GetTripTotal(start, end, campground.DailyFee).ToString("C");

					if (campgrounds.Count > 1)
					{
						line = campground.Name.PadRight(32) + line;
					}
					line = $"{menuNumber,2})  " + line;
					screenOutput.Add(line);
					allSites.Add(site);
					menuNumber++;
				}
			}
			if (screenOutput.Count > 1)
			{
				foreach (string line in screenOutput)
				{
					Console.WriteLine(line);
				}
			}

			Console.WriteLine();
			return allSites;
		}

		private static void ResizeAndExitWindow()
		{
			Console.WriteLine();
			Console.WriteLine("BYE-BYE :)");
			System.Threading.Thread.Sleep(400);
			Console.Beep(1307, 75);
			Console.SetWindowSize(104, 24);
			System.Threading.Thread.Sleep(400);
			Console.Beep(1300, 75);
			Console.SetWindowSize(52, 12);
			System.Threading.Thread.Sleep(400);
			Console.Beep(1107, 75);
			Console.SetWindowSize(26, 6);
			System.Threading.Thread.Sleep(400);
			Console.Beep(907, 75);
			Console.SetWindowSize(13, 3);
		}
	}
}
