using System;
using System.Linq;
using System.Text;
using Capstone.DAL;
using Capstone.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Capstone
{
    class Class1
    {
        private static ReservationDAL resDAL = new ReservationDAL(ConnectionString);
        private const string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";

        public void PrintUpcommingReservations(Park currentPark)
        {
            List<Reservation> listOfReservations = resDAL.GetReservations(currentPark);
            string header = $"{"Reservation ID",-15}{"Site ID",-14}{"Name",-30}{"From",-10}{"To",-13}{"Booked On",0}\n";
            Console.WriteLine(header);
            foreach (Reservation reservation in listOfReservations)
            {
                Console.WriteLine(reservation.ToString());
            }
            Console.Write("Press Any Key To Continue");
            Console.ReadLine();
        }

    }
}
