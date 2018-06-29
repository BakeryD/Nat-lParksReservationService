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
        /// Initializes the program interface
        /// </summary>
        public void Run()
        {
            PrintHeader();
            // Print the list of Parks
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
            Console.Write("                             ");
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

        private void MainMenu()
        {

            while (true)
            {
                int input = PrintMainMenu();
                List<Park> parks = parkDAL.GetParks();

                if (input == parks.Count + 1)
                {
                    Console.Clear();
                    ResizeAndExitWindow();
                    return;
                }
                else
                {
                    ParkMenu(parks[input - 1]);
                }
            }
        }

        private static int PrintMainMenu()
        {
            Console.WriteLine("Pick a Park to View");
            Console.WriteLine();
            //Call ParkDAL to get all the parks
            //Get a list of park objects from ParkDAL
            List<Park> parks = parkDAL.GetParks();

            //Choice number displayed
            for (int i = 1; i <= parks.Count; i++)
            {
                Console.WriteLine($" {i}) {parks[i - 1].Name}");
            }
            Console.WriteLine($" {parks.Count + 1}) Quit");
            Console.WriteLine();
            Console.Write(">");
            return CLIHelper.GetAnInteger(1, parks.Count + 1);
        }

        private void ParkMenu(Park currentPark)
        {
            Console.Clear();
            int input;
            do
            {
                input = PrintParkMenu(currentPark);
                switch (input)
                {
                    case 1:
                        ViewCampGroundsMenu(currentPark);
                        break;
                    case 2:
                        SearchForReservation(currentPark, true);
                        break;

                }
            } while (input != 3);
            if (input == 3)
            {
                Console.Clear();
            }
        }

        private static int PrintParkMenu(Park currentPark)
        {
            Console.Clear();
            Console.WriteLine(currentPark.ToString());
            Console.WriteLine();
            Console.WriteLine("Choose an option");
            Console.WriteLine(" 1) View Campgrounds");           // View Campgrounds in this park
            Console.WriteLine(" 2) Search for Reservation");     //2) Search for Reservation in this park
            Console.WriteLine(" 3) Return to Previous Screen");  // 3) Return to Previous Screen
            Console.WriteLine();
            Console.Write(">");


            int userChoice = CLIHelper.GetAnInteger(1, 3);


            return userChoice;
        }

        private void ViewCampGroundsMenu(Park currentPark)
        {
            Console.Clear();
            int input;
            do
            {
                input = PrintCampgroundMenu(currentPark);
                switch (input)
                {
                    case 1:
                        SearchForReservation(currentPark, false);
                        break;
                        //ADD ADVANCED SEARCH OPTION
                }
            } while (input != 2);
        }

        private int PrintCampgroundMenu(Park currentPark)
        {
            PrintCampgroundList(currentPark);
            Console.WriteLine("Choose an option");
            Console.WriteLine("   1) Search for Available Reservation");
            Console.WriteLine("   2) Return to Previous Screen");
            Console.WriteLine();
            Console.Write("  >");
            int userChoice = CLIHelper.GetAnInteger(1, 2);

            return userChoice;
        }

        private void SearchForReservation(Park currentPark, bool fromWholePark)
        {
            Console.Clear();

            List<Campground> campgrounds = new List<Campground>();
            int occupants = 1;
            bool isAccessible = false;
            int RVLength = 0;
            bool hasUtilities = false;

            int input = -1;
            if (!fromWholePark)
            {
                Console.Clear();
                Console.WriteLine($"Campgrounds in {currentPark.Name}");
                campgrounds = PrintCampgroundList(currentPark);

                Console.Write("Which Campground (enter 0 to cancel)? ");
                input = CLIHelper.GetAnInteger(0, campgrounds.Count);
            }
            if (input != 0)
            {
                Console.Write(">Enter a Start Date for Reservation:  ");
                DateTime reservationStart = CLIHelper.GetDateTime(DateTime.Now.Date);
                Console.Write(">Enter a Departure Date for Reservation:  ");
                DateTime reservationEnd = CLIHelper.GetDateTime(reservationStart);

                Console.Write("Would you like to preform an Advanced Search? (Y/N):  ");
                bool isAdvancedSearch = CLIHelper.GetBoolean();
                if (isAdvancedSearch)
                {
                    Console.WriteLine();
                    Console.Write("What Number of Occupants:  ");
                    occupants = CLIHelper.GetAnInteger(1, 55);
                    Console.Write("Wheelchair Accessible? (Y/N):  ");
                    isAccessible = CLIHelper.GetBoolean();
                    Console.Write("Maximum RV Length Required:  ");
                    RVLength = CLIHelper.GetAnInteger(0, 35);
                    Console.Write("Utilities Required? (Y/N):  ");
                    hasUtilities = CLIHelper.GetBoolean();
                }


                if (fromWholePark)
                {
                    BookAReservation(reservationStart, reservationEnd, campgrounds, occupants, isAccessible, RVLength, hasUtilities);
                }
                else
                {
                    BookAReservation(reservationStart, reservationEnd, new List<Campground>() { campgrounds[input - 1] }, occupants, isAccessible, RVLength, hasUtilities);
                }

            }

        }

        private List<Campground> PrintCampgroundList(Park currentPark)
        {
            List<Campground> campgrounds = campDAL.GetCampgrounds(currentPark);
            Console.WriteLine("     Name                  Open            Close        DailyFee");
            for (int i = 1; i <= campgrounds.Count; i++)
            {
                Console.WriteLine($"#{i}   " + campgrounds[i - 1].ToString());
            }
            Console.WriteLine();
            return campgrounds;
        }

        private void BookAReservation(DateTime reservationStart, DateTime reservationEnd, List<Campground> campgrounds, int occupants, bool isAccessible, int RVLength, bool hasUtilities)
        {
            List<Site> sites = PrintSiteList(reservationStart, reservationEnd, campgrounds, occupants, isAccessible, RVLength, hasUtilities);
            //if site list is empty, ask for new date, call print site list again until there are sites in the list

            while (sites.Count == 0)
            {
                Console.WriteLine("No Available Sites For That Date Range, Try Again.");
                Console.WriteLine();
                Console.Write(">Enter a Start Date for Reservation:  ");
                DateTime newStart = CLIHelper.GetDateTime(DateTime.Now.Date);
                Console.Write(">Enter a Departure Date for Reservation:  ");
                DateTime newEnd = CLIHelper.GetDateTime(newStart);
                sites = PrintSiteList(newStart, newEnd, campgrounds, occupants, isAccessible, RVLength, hasUtilities);

            }


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
                    FromDate = reservationStart,
                    ToDate = reservationEnd
                };

                int resId = resDAL.MakeReservation(reservation);

                Console.WriteLine();
                Console.WriteLine($"The Reservation has been made and your confirmation Id is {resId}");
            }
        }

        private List<Site> PrintSiteList(DateTime start, DateTime end, List<Campground> campgrounds, int occupants, bool isAccessible, int RVLength, bool hasUtilities)
        {
            List<string> screenOutput = new List<string>();
            List<Site> allSites = new List<Site>();
            int menuNumber = 1;
            string header = "Site No.    Max Occup.   Accessible?     Max RV Length        Utility     Cost";
            if (campgrounds.Count > 1)
            {
                header = "Campground".PadRight(10) + header;
            }
            screenOutput.Add(header);

            foreach (Campground campground in campgrounds)
            {
                List<Site> sites = siteDAL.FindAvailableSitesAdvanced(start, end, campground, occupants, isAccessible, RVLength, hasUtilities);
                foreach (Site site in sites)
                {
                    string line = site.ToString() + CLIHelper.GetTripTotal(start, end, campground.DailyFee).ToString("C");

                    if (campgrounds.Count > 1)
                    {
                        line = campground.Name.PadRight(10) + line;
                    }
                    line = $"{menuNumber})  " + line;
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
