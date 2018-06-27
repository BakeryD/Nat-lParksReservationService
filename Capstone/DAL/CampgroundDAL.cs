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

		/// <summary>
		/// Returns a list of campgrounds in a given park
		/// </summary>
		/// <param name="fromPark">The park to look in</param>
		/// <returns></returns>
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
						// Create a new campground
                        Campground campground = new Campground();

						campground.CampgroundId = Convert.ToInt32(reader["campground_id"]);
						campground.Name = Convert.ToString(reader["name"]);
						campground.ParkId = Convert.ToInt32(reader["park_id"]);
						campground.OpenMonth = Convert.ToInt32(reader["open_from_mm"]);
						campground.CloseMonth = Convert.ToInt32(reader["open_to_mm"]);
						campground.DailyFee = Convert.ToInt32(reader["daily_fee"]);

						// Add it to the list
						campgrounds.Add(campground);
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
