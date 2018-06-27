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





        /// <summary>
        /// Initializes the program interface
        /// </summary>
        public void Run()
        {
            PrintHeader();
            // Print the list of Parks
            PrintMainMenu();
            // Accept User input
            string userChoice = Console.ReadLine().ToUpper();       //User selects a park to get more info on

            // Call print park menu based on user input
            PrintParkMenu(userChoice);                              //
            userChoice = Console.ReadLine().ToUpper();

            // call print campground menu based on user input
            PrintCampGroundMenu(userChoice);


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

        public void PrintParkMenu(string choice)
        {

          



        }
        /// <summary>
        /// Displays a list of available parks with the option to get more info.
        /// </summary>
        public void PrintMainMenu()
        {
                 // SQL time!
            //Call ParkDAL to get all the parks
            //Get a list of park objects from ParkDAL
            List<Park> parks=new List<Park>();

            //Choice number displayed
            int menuOption = 1;
            foreach (var park in parks)
            { 
                Console.WriteLine($"{menuOption}) {park.Name}");
                menuOption++;
            }

            Console.WriteLine();
            Console.WriteLine();
        }


    }

}
