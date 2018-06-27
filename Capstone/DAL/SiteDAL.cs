using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SiteDAL
    {
		private readonly string ConnectionString;
		
		/// <summary>
		/// Initializes the DAL class with the connection string for our database.
		/// </summary>
		/// <param name="connectString">Connection string to database.</param>
		public SiteDAL(string connectionString)
		{
			ConnectionString = connectionString;
		}

		/// <summary>
		/// Returns a list of sites in a given campground
		/// </summary>
		/// <param name="fromPark">The park to look in</param>
		/// <returns></returns>
		public IList<Site> GetSites(Campground fromCampground)
		{
			//Create an output list
			List<Site> sites = new List<Site>();

			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					//Open connection to database
					conn.Open();

					//Create query to get all campgrounds from the specified park
					string sql = $"Select * From site Where site.campground_id={fromCampground.CampgroundId};";
					SqlCommand cmd = new SqlCommand(sql, conn);

					//Execute Command
					SqlDataReader reader = cmd.ExecuteReader();

					//Loop through the rows and create Campground Objects
					while (reader.Read())
					{
						// Create a new campground
						Site site = new Site();
						site.SiteId = Convert.ToInt32(reader["site_id"]);
						site.Number = Convert.ToInt32(reader["site_number"]);
						site.CampgroundId = Convert.ToInt32(reader["campground_id"]);
						site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
						site.HandicapAccessible = Convert.ToBoolean(reader["accessible"]);
						site.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
						site.Utilities = Convert.ToBoolean(reader["utilities"]);

						// Add it to the list
						sites.Add(site);
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
			return sites;
		}
	}
}
