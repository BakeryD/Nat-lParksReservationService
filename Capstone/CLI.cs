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
		const int Command_ViewCampgrounds = 1;
		const int Command_SearchParkReservation = 2;
		const int Command_ViewReservations = 3;
		const int Command_ViewCurrentCampers = 4;
		const int Command_QuitParkMenu = 5;
		const int Command_SearchCampgroundReservation = 1;
		const int Command_QuitCampgroundMenu = 2;
		
		/// <summary>
		/// Runs the program
		/// </summary>
		public void Run()
		{
			Console.Title = "Parks Reservation Module";
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
			Console.WriteLine(@"      ,-.----.                                 ");
			Console.WriteLine(@"      \    /  \                           ,-.  ");
			Console.WriteLine(@"      |   :    \                      ,--/ /|  ");
			Console.WriteLine(@"      |   |  .\ :            __  ,-.,--. :/ |  ");
			Console.WriteLine(@"      .   :  |: |          ,' ,'/ /|:  : ' /   ");
			Console.WriteLine(@"      |   |   \ : ,--.--.  '  | |' ||  '  /    ");
			Console.WriteLine(@"      |   : .   //       \ |  |   ,''  |  :    ");
			Console.WriteLine(@"      ;   | |`-'.--.  .-. |'  :  /  |  |   \   ");
			Console.WriteLine(@"      |   | ;    \__\/: . .|  | '   '  : |. \  ");
			Console.WriteLine(@"      :   ' |    ,' .--.; |;  : |   |  | ' \ \ ");
			Console.WriteLine(@"      :   : :   /  /  ,.  ||  , ;   '  : |--'  ");
			Console.WriteLine(@"      |   | :  ;  :   .'   \---'    ;  |,'     ");
			Console.WriteLine(@"      `---'.|  |  ,     .-./        '--'       ");
			Console.WriteLine(@"        `---`   `--`---'                       ");
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("        Populating Forests......");

			Console.ForegroundColor = ConsoleColor.DarkGreen;
			for (int i = 0; i < 10; i++)
			{
				Console.SetCursorPosition(2 + i * 5, 20);
				Console.Write(@" /\ ");
				Console.SetCursorPosition(2 + i * 5, 21);
				Console.Write(@"/  \");
				Console.SetCursorPosition(2 + i * 5, 22);
				Console.Write(@"/  \");
				Console.SetCursorPosition(2 + i * 5, 23);
				Console.Write(@"/__\");
				Console.SetCursorPosition(2 + i * 5, 24);
				Console.Write(@" || ");
				System.Threading.Thread.Sleep(200);
			}

			Console.ForegroundColor = ConsoleColor.White;
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
					case Command_ViewCampgrounds:     // Serach a specific campground in the park
						CampgroundMenu(currentPark);
						break;
					case Command_SearchParkReservation:     // Search Parkwide for a reservation
						SearchForReservation(currentPark, true);
						break;
					case Command_ViewReservations:     //View all upcomming reservations in the next 30 days
						PrintUpcommingReservations(currentPark);
						break;
					case Command_ViewCurrentCampers:
						PrintCurrentCampers(currentPark);
						break;

				}
			} while (input != Command_QuitParkMenu);   // Continue until the user enters the quit command
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
			Console.WriteLine(" 3) View Upcoming Reservations");
			Console.WriteLine(" 4) View Current Campers");
			Console.WriteLine(" 5) Return to Previous Screen");
			Console.Write(" > ");

			// Return user input
			return CLIHelper.GetAnInteger(Command_ViewCampgrounds, Command_QuitParkMenu);
		}

		/// <summary>
		/// Prints a list of all upcomming reservations in the next 30 days in the park.
		/// </summary>
		/// <param name="currentPark">The park to find reservations in.</param>
		public void PrintUpcommingReservations(Park currentPark)
		{
			Console.Clear();

			//Get a list of upcoming reservaitons
			List<Reservation> listOfReservations = resDAL.GetReservations(currentPark, false);

			// Print a header
			Console.WriteLine($"Showing reservations in {currentPark.Name} in the next 30 days:\n");
			Console.WriteLine($"{" Reservation ID",-16}{"Site ID",-10}{"Name",-30}{"From",-13}{"To",-13}{"Booked On",0}\n");

			// Print each reservations
			foreach (Reservation reservation in listOfReservations)
			{
				Console.WriteLine(" " + reservation.ToString());
			}

			Console.WriteLine();
			Console.Write("Press Any Key To Continue ");
			Console.ReadKey(true);
		}

		/// <summary>
		/// Prints a list of all current camper reservations in the given park
		/// </summary>
		/// <param name="currentPark">The park to look in</param>
		private void PrintCurrentCampers(Park currentPark)
		{
			Console.Clear();

			//Get a list of upcoming reservaitons
			List<Reservation> listOfReservations = resDAL.GetReservations(currentPark, true);

			// Print a header
			Console.WriteLine($"Showing current camping reservations in {currentPark.Name}:\n");
			Console.WriteLine($"{" Reservation ID",-16}{"Site ID",-10}{"Name",-30}{"From",-13}{"To",-13}{"Booked On",0}\n");

			// Print each reservations
			foreach (Reservation reservation in listOfReservations)
			{
				Console.WriteLine(" " + reservation.ToString());
			}

			Console.WriteLine();
			Console.Write("Press Any Key To Continue ");
			Console.ReadKey(true);
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
					case Command_SearchCampgroundReservation:
						SearchForReservation(currentPark, false);
						break;
				}
			} while (input != Command_QuitCampgroundMenu);
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
			return CLIHelper.GetAnInteger(Command_SearchCampgroundReservation, Command_QuitCampgroundMenu);
		}

		/// <summary>
		/// Preforms a search for campsites available to be reserved
		/// </summary>
		/// <param name="currentPark">The park to find a reservation in.</param>
		/// <param name="fromWholePark">Option to search through all campgrounds in the specified Park.</param>
		private void SearchForReservation(Park currentPark, bool fromWholePark)
		{
			Console.Clear();

			//Initialize working variables
			List<Campground> campgrounds = campDAL.GetCampgrounds(currentPark);
			List<Site> sites = new List<Site>();
			DateTime startDate = DateTime.MinValue;
			DateTime endDate = DateTime.MinValue;
			int input = -1;
			bool continueSearching = true;

			do
			{
				//If the reservation is for a specific campground
				if (!fromWholePark)
				{
					campgrounds = campDAL.GetCampgrounds(currentPark);
					Console.WriteLine($"Campgrounds in {currentPark.Name}");
					PrintCampgroundList(currentPark);   //Print list of campgrounds in park to screen

					//Ask for user choice
					Console.Write("Which Campground (enter 0 to cancel)? ");
					input = CLIHelper.GetAnInteger(0, campgrounds.Count);

					//If User chose a campground, make the list only contain their choice
					if (input != 0)
					{
						campgrounds = new List<Campground>() { campgrounds[input - 1] };
					}
				}
				//initialize advanced search criteria

				if (input != 0)
				{
					int occupants = 1;
					bool isAccessible = false;
					int RVLength = 0;
					bool hasUtilities = false;
					//Get reservation dates in correct format
					Console.Write(">Enter a Start Date for Reservation:  ");
					startDate = CLIHelper.GetDateTime(DateTime.Now.Date);
					Console.Write(">Enter a Departure Date for Reservation:  ");
					endDate = CLIHelper.GetDateTime(startDate);

					//Ask user for optional advanced search
					Console.Write("Would You Like to Preform an Advanced Search? (Y/N):  ");
					bool isAdvancedSearch = CLIHelper.GetBoolean();

					//Get user specified advanced search criteria
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
					//Print a list of sites that match the search criteria
					sites = PrintSiteList(startDate, endDate, campgrounds, occupants, isAccessible, RVLength, hasUtilities);

					//if there are no sites ask to try again or quit
					if (sites.Count == 0)
					{
						Console.Clear();
						Console.WriteLine("No Available Sites per Your Specifications.");
						Console.Write("Would You Like to Try Again? (Y/N): ");
						continueSearching = CLIHelper.GetBoolean();
						Console.WriteLine();
					}
				}
				//Loop while the search return empty and user has not chosen to continue/quit
			} while (sites.Count == 0 && input != 0 && continueSearching);

			//Book a reservation if sites were found 
			if (sites.Count != 0)
			{
				BookAReservation(sites, startDate, endDate);
			}
		}

		/// <summary>
		/// Prints the list of campgrounds in the current park.
		/// </summary>
		/// <param name="currentPark">The park to look for campgrounds in.</param>
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

		/// <summary>
		/// Books a reservation at site of choice.
		/// </summary>
		/// <param name="sites">A list of sites to choose from.</param>
		/// <param name="startDate">Start date of the reservation.</param>
		/// <param name="endDate">Reservation end date.</param>
		private void BookAReservation(List<Site> sites, DateTime startDate, DateTime endDate)
		{
			//Get user choice of site
			Console.WriteLine("Which site should be reserved (enter 0 to cancel) ");
			int input = CLIHelper.GetAnInteger(0, sites.Count);

			//If they chose a site number, get their name and book a reservation
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

				//Log reservation in the database 
				int resId = resDAL.MakeReservation(reservation);

				//Show the user their confirmation ID
				Console.WriteLine();
				Console.WriteLine($"The Reservation has been made and your confirmation Id is {resId}");
				Console.WriteLine("Press any key to continue");
				Console.ReadKey(true);
				Console.Clear();
			}
		}

		/// <summary>
		/// Prints a list of sites matching given criteria
		/// </summary>
		/// <param name="startDate">Reservation start date</param>
		/// <param name="endDate">Reservation end date</param>
		/// <param name="campgrounds">List of campgrounds to search in</param>
		/// <param name="occupants">Required number of occupants</param>
		/// <param name="isAccessible">Desired handicap accessibility</param>
		/// <param name="RVLength">Length of RV </param>
		/// <param name="hasUtilities">Desired existance of utilities</param>
		/// <returns>The list of sites.</returns>
		private List<Site> PrintSiteList(DateTime startDate, DateTime endDate, List<Campground> campgrounds, int occupants, bool isAccessible, int RVLength, bool hasUtilities)
		{
			// Initialize working variables
			List<string> screenOutput = new List<string>();
			List<Site> allSites = new List<Site>();
			int menuNumber = 1;

			// Create a header and add it to the screen output
			string header = "Site No.  Max Occup.  Accessible?  Max RV Length  Utility  Cost";
			if (campgrounds.Count > 1)
			{
				header = "Campground".PadRight(32) + header;
			}
			header = "     " + header;
			screenOutput.Add(header);

			// Find each relevant site in each campsite
			foreach (Campground campground in campgrounds)
			{
				List<Site> sites = siteDAL.FindAvailableSites(startDate, endDate, campground, occupants, isAccessible, RVLength, hasUtilities);
				foreach (Site site in sites)
				{
					// Creaete a line to display info about each site and add it to the screen output
					string line = site.ToString() + CLIHelper.GetTripTotal(startDate, endDate, campground.DailyFee).ToString("C");

					if (campgrounds.Count > 1)
					{
						line = campground.Name.PadRight(32) + line;
					}
					line = $"{menuNumber,2})  " + line;
					screenOutput.Add(line);

					// Add the site to the output list
					allSites.Add(site);

					// Increment the number next to the site
					menuNumber++;
				}
			}

			// If there are sites, write them to the screen
			if (allSites.Count != 0)
			{
				foreach (string line in screenOutput)
				{
					Console.WriteLine(line);
				}
			}
			Console.WriteLine();

			// Return the list of found sites
			return allSites;
		}

		/// <summary>
		/// Shrink the screen to say goodbye
		/// </summary>
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
