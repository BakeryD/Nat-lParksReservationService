using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ParkDAL
    {
        private readonly string ConnectionString;

        /// <summary>
        /// Initializes the DAL class with the connection string for our database.
        /// </summary>
        /// <param name="connectString">Connection string to database.</param>
        public ParkDAL(string connectString)
        {
            ConnectionString = connectString;
        }

		/// <summary>
		/// Gets a list of all of the parks
		/// </summary>
		/// <returns></returns>
		public List<Park> GetParks()
		{
			// Create a list to hold output
			List<Park> parks = new List<Park>();

			try
			{
				// Create new connection object
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					// Open the conneciton
					conn.Open();

					// Create a command
					string sql = "SELECT * FROM park ORDER BY name ASC;";
					SqlCommand cmd = new SqlCommand(sql, conn);

					// Execute the command
					SqlDataReader reader = cmd.ExecuteReader();

					// Loop through each row
					while (reader.Read())
					{
						// Create a new park
						Park park = new Park();
						park.ParkId = Convert.ToInt32(reader["park_id"]);
						park.Name = Convert.ToString(reader["name"]);
						park.Location = Convert.ToString(reader["location"]);
						park.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
						park.AreaInSqKm = Convert.ToInt32(reader["area"]);
						park.Visitors = Convert.ToInt32(reader["visitors"]);
						park.Description = Convert.ToString(reader["description"]);

						// Add it to the output list
						parks.Add(park);
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}

			// Return the output list
			return parks;
		}
    }
}
