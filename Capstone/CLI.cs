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
        private Park CurrentPark;
        private Campground CurrentGround;
        private static ParkDAL parkDAL = new ParkDAL(ConnectionString);
        private static CampgroundDAL campDAL = new CampgroundDAL(ConnectionString);




        /// <summary>
        /// Initializes the program interface
        /// </summary>
        public void Run()       //  SHOULD CALL A MAIN MENU "WRAPPER METHOD"
        {
            PrintHeader();
            // Print the list of Parks
            CurrentPark = PrintMainMenu();
            GetParkInfo(CurrentPark);

            if (CurrentPark == null)
            {
                return;
            } // If they quit, current park stays null and this ends the program
              // Call print park menu based on user input
           CurrentGround= PrintParkMenu(CurrentPark);                              // User gets park info and choice to view campgrounds or search for reservation

            // call print campground menu based on user input
            //  PrintCampGroundMenu(userChoice);


        }

        /// <summary>
        /// Header/Welcome text for the main menu
        /// </summary>
        private void PrintHeader()
        {
            Console.WriteLine("Welcome to the National Parks Reservation System!");
        }

        public void PrintCampGroundMenu(Campground campground)
        {

        }

        public int GetParkInfo(Park park)
        {
            Console.Clear();
            Console.WriteLine(park.ToString());
            Console.WriteLine();
            Console.WriteLine("Choose an option");
            Console.WriteLine("1) View Campgrounds");    // View Campgrounds in this park
            Console.WriteLine("2) Search for Reservation");    //2) Search for Reservation in this park
            Console.WriteLine("3) Return to Previous Screen");    // 3) Return to Previous Screen



            int userChoice = GetInteger(Console.ReadLine());


            return userChoice;


        }
        /// <summary>
        /// Displays a list of campgrounds at the specified park with options to select a site, search for availability, or quit.
        /// </summary>
        /// <param name="chosenPark">User selected park.</param>
        public Campground PrintParkMenu(Park chosenPark)
        {
            Console.Clear();
            Console.WriteLine(CurrentPark.ToString());
            Console.WriteLine();
            var campgrounds=campDAL.GetCampgrounds(chosenPark); // Get a list of campgrounds at the specified park

            //User can view campgrounds in park or search for availability in a specified date range
            Console.WriteLine($"Available Campgrounds in {chosenPark.Name}");
            Console.WriteLine();
            int option = 1;
            foreach (var campground in campgrounds)
            {
                Console.WriteLine($"{option}) {campground.ToString()}");
                option++;
            }

            Console.WriteLine();
            Console.WriteLine("Choose an option");
            Console.WriteLine("0) Return to Main Menu");
            Console.WriteLine("1) Book Reservation (Any Campground in Park)");
            Console.WriteLine("2) View Available Reservations (By Campground)");

            int userChoice = GetInteger(Console.ReadLine());


            return campgrounds[0];
        }
        /// <summary>
        /// Displays a list of available parks with the option to get more info and returns the result
        /// </summary>
        public Park PrintMainMenu()      // Make this return the chosen park
        {
            bool viewMenu = true;
            while (viewMenu)
            {
                Console.WriteLine("Available Parks");
                Console.WriteLine();
                //Call ParkDAL to get all the parks
                //Get a list of park objects from ParkDAL
                IList<Park> parks = parkDAL.GetParks();

                //Choice number displayed
                int menuOption = 1;
                foreach (var park in parks)
                {
                    Console.WriteLine($"{menuOption}) {park.Name}");
                    menuOption++;
                }   //Display all of the parks

                Console.WriteLine("Q) Quit");
                Console.WriteLine();

                string userChoice = Console.ReadLine().ToUpper();       //User selects a park to get more info on
                if (userChoice == "Q")                    //Quit option
                {
                    return null;
                }
                var choiceNum = GetInteger(userChoice);     // If not quit, it needs to be a number
                if (choiceNum > parks.Count - 1)
                {
                    while (choiceNum > parks.Count - 1)
                    {
                        Console.WriteLine("Try Again.");
                        choiceNum = GetInteger(Console.ReadLine());
                    }

                }              // Within the range of possible parks
                if (choiceNum <= 0)
                {
                    while (choiceNum < 0)
                    {
                        Console.WriteLine("Try Again.");
                        choiceNum = GetInteger(Console.ReadLine());
                    }
                }                               //  ""
                Park infoPark = parks[choiceNum];
               int infoChoice= GetParkInfo(infoPark);              // Prints Park info

                switch (infoChoice)
                {
                    case 1:




                    default:
                        break;
                }



            }
            return infoPark;
        }

        public static int GetInteger(string message)
        {
            string userInput = message;
            int intValue = 0;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid Menu Choice, Try Again.");
                }

                if (int.TryParse(message, out intValue))
                {
                    return intValue;
                }
                //Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!int.TryParse(userInput, out intValue));

            return intValue;

        }



    }

}
