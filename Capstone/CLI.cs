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




        /// <summary>
        /// Initializes the program interface
        /// </summary>
        public void Run()
        {
            PrintHeader();
            // Print the list of Parks
           CurrentPark = PrintMainMenu();
            if (CurrentPark==null)
            {
                return;
            } // If they quit, current park stays null and this ends the program
           // Call print park menu based on user input
            PrintParkMenu(CurrentPark);                              // User gets park info and choice to view campgrounds or search for reservation
           //   userChoice = Console.ReadLine().ToUpper();

            // call print campground menu based on user input
            //  PrintCampGroundMenu(userChoice);


        }

        /// <summary>
        /// Header/Welcome text for the main menu
        /// </summary>
        private void PrintHeader()
        {
            Console.WriteLine();
        }

        public void PrintCampGroundMenu(string userChoice)
        {
            
        }

        public void PrintParkMenu(Park chosenPark)
        {
            //CurrentPark.ToString();



            //User can view campgrounds in park or search for availability in a specified date range
          



        }
        /// <summary>
        /// Displays a list of available parks with the option to get more info and returns the result
        /// </summary>
        public Park PrintMainMenu()      // Make this return the chosen park
        {
            Console.WriteLine("Available Parks");
            Console.WriteLine();
                 // SQL time!
            //Call ParkDAL to get all the parks
            //Get a list of park objects from ParkDAL
            IList<Park> parks= parkDAL.GetParks();

            //Choice number displayed
            int menuOption = 1;
            foreach (var park in parks)
            { 
                Console.WriteLine($"{menuOption}) {park.Name}");
                menuOption++;
            }

            Console.WriteLine("Q) Quit");
            Console.WriteLine();

            string userChoice = Console.ReadLine().ToUpper();       //User selects a park to get more info on
            if (userChoice=="Q")                    //Quit option
            {
                return null;
            }

            var choiceNum = GetInteger(userChoice);     // If not quit, it needs to be a number

             if (choiceNum > parks.Count-1)
             {
                while (choiceNum>parks.Count-1)
                {
                    Console.WriteLine("Try Again.");
                    choiceNum = GetInteger(Console.ReadLine());
                }

             }              // Within the range of possible parks
            if (choiceNum<0)
            {
                while (choiceNum < 0)
                {
                    Console.WriteLine("Try Again.");
                    choiceNum = GetInteger(Console.ReadLine());
                }
            }                               //  ""


            Park infoPark = parks[choiceNum];       


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
