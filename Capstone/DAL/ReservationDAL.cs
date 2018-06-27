using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
	public class ReservationDAL
	{
		private readonly string ConnectionString;

		/// <summary>
		/// Initializes the DAL class with the connection string for our database.
		/// </summary>
		/// <param name="connectString">Connection string to database.</param>
		public ReservationDAL(string connectionString)
		{
			ConnectionString = connectionString;
		}

		/// <summary>
		/// Returns a list of reservations for a given site
		/// </summary>
		/// <param name="fromSite">The site to look in</param>
		/// <returns></returns>
		public IList<Reservation> GetReservations(Site fromSite)
		{
			//Create an output list
			List<Reservation> reservations = new List<Reservation>();

			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					//Open connection to database
					conn.Open();

					//Create query to get all campgrounds from the specified park
					string sql = $"Select * From reservation Where reservation.site_id = {fromSite.SiteId};";
					SqlCommand cmd = new SqlCommand(sql, conn);

					//Execute Command
					SqlDataReader reader = cmd.ExecuteReader();

					//Loop through the rows and create Campground Objects
					while (reader.Read())
					{
						// Create a new campground
						Reservation reservation = new Reservation();
						reservation.ReservationId = Convert.ToInt32(reader["reservation_id"]);
						reservation.SiteId = Convert.ToInt32(reader["site_id"]);
						reservation.Name = Convert.ToString(reader["name"]);
						reservation.FromDate = Convert.ToDateTime(reader["from_date"]);
						reservation.ToDate = Convert.ToDateTime(reader["to_date"]);
						reservation.CreatedDate = Convert.ToDateTime(reader["create_date"]);

						// Add it to the list
						reservations.Add(reservation);
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
			return reservations;
		}

		/// <summary>
		/// Returns a list of campsites that are available for the given date and time
		/// </summary>
		/// <param name="startDate">The requested start date</param>
		/// <param name="endDate">The requested end date</param>
		/// <param name="campground">The requested campground</param>
		/// <returns></returns>
		public IList<Site> FindAvailableReservations(DateTime startDate, DateTime endDate, Campground campground)
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
					string sql = $"SELECT TOP 5 * FROM site " +
								 $"INNER JOIN reservation ON site.site_id = reservation.site_id " +
								 $"WHERE site.campground_id = {campground.CampgroundId} AND " +
								 $"(reservation.from_date < {startDate.Date} AND reservation.to_date < {endDate.Date}) OR " +
								 $"(reservation.from_date > {startDate.Date} AND reservation.to_date > {endDate.Date});";

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

		/// <summary>
		/// Enters a new reservation into the database
		/// </summary>
		/// <param name="reservation">The reservation to enter</param>
		/// <returns>The reservation Id or 0 if it fails</returns>
		public int MakeReservation(Reservation reservation)
		{
			// Initialize output variable
			int id = 0;
			try
			{
				// Create new connection object
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					// Open the conneciton
					conn.Open();

					// Create a command
					string insert = $"INSERT INTO reservation (site_id, name, from_date, to_date) VALUES ({reservation.SiteId}, '{reservation.Name}', '{reservation.FromDate}', '{reservation.ToDate}');";
					SqlCommand cmd = new SqlCommand(insert, conn);

					// Execute the command
					cmd.ExecuteNonQuery();

					// Create a query to find the new reservation's id
					string query = "SELECT MAX(reservation_id) FROM reservation;";
					cmd.CommandText = query;
					id = Convert.ToInt32(cmd.ExecuteScalar());
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			// Return the new reservation's id or 0 if failed
			return id;
		}

	}
}