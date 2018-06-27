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
		/// Returns a list of reservatoins for a given site
		/// </summary>
		/// <param name="fromPark">The park to look in</param>
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
					string sql = $"Select * From reservations Where reservations.site_id = {fromSite.SiteId};";
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


	}
}
