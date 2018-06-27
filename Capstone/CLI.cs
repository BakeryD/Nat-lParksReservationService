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

        /// <summary>
        /// Initializes the program interface
        /// </summary>
        public void Run()
        {
            PrintMainMenu();

            // Accept User input
            string userChoice = Console.ReadLine().ToUpper();

            // Call print park menu based on user input
            PrintParkMenu(userChoice);
            userChoice = Console.ReadLine().ToUpper();

            // call print campground menu based on user input
            PrintCampGroundMenu(userChoice);


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


            //Create a list of park objects?

            //Select * From Park




        }


    }

}
