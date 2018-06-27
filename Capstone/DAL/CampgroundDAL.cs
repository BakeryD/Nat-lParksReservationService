using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
   public class CampgroundDAL
    {
        private readonly string ConnectionString;
        /// <summary>
        /// Initializes the DAL class with the connection string for our database.
        /// </summary>
        /// <param name="connectString">Connection string to database.</param>
        public CampgroundDAL (string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IList<Campground> GetCampgrounds(Park fromPark)
        {
            //Create an output list
            List<Campground> campgrounds = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    //Open connection to database
                    conn.Open();
                    //Create query to get all campgrounds from the specified park
                    string sql = $"Select * From campground Where campground.park_id={fromPark.ParkId};";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    //Execute Command
                    SqlDataReader reader = cmd.ExecuteReader();
                    //Loop through the rows and create Campground Objects
                    while (reader.Read())
                    {
                        Campground campground = new Campground();
                    }

                }


            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return campgrounds;

        }
    }
}
