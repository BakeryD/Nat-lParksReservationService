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
		public List<Site> GetSites(Campground fromCampground)
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
			return sites;
		}

		/// <summary>
		/// Returns a list of campsites that are available for the given date and time
		/// </summary>
		/// <param name="startDate">The requested start date</param>
		/// <param name="endDate">The requested end date</param>
		/// <param name="campground">The requested campground</param>
		/// <returns></returns>
		public List<Site> FindAvailableSites(DateTime startDate, DateTime endDate, Campground campground)
		{
			//Create an output list
			List<Site> sites = new List<Site>();

			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					//Open connection to database
					conn.Open();

					//Create query to get all site from the specified campground
					string sql = $"SELECT DISTINCT TOP 5  site.* FROM campground " +
								 $"INNER JOIN site ON campground.campground_id = site.campground_id " +
								 $"INNER JOIN reservation ON site.site_id = reservation.site_id " +
								 $"WHERE site.campground_id = {campground.CampgroundId} AND campground.open_from_mm <= {startDate.Month} AND campground.open_to_mm >= {endDate.Month} AND" +
								 $"((reservation.from_date < '{startDate.ToString("yyyy-MM-dd")}' AND reservation.to_date < '{endDate.ToString("yyyy-MM-dd")}') OR " +
								 $"(reservation.from_date > '{startDate.ToString("yyyy-MM-dd")}' AND reservation.to_date > '{endDate.ToString("yyyy-MM-dd")}'));";

					SqlCommand cmd = new SqlCommand(sql, conn);

					//Execute Command
					SqlDataReader reader = cmd.ExecuteReader();

					//Loop through the rows and create site Objects
					while (reader.Read())
					{
						// Create a new site
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
