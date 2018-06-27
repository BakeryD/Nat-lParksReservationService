﻿using System;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using Capstone.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Models;

namespace Capstone.Tests
{
	[TestClass]
	public class ReservationDALTests :CampingTests
	{
		[DataTestMethod]
		[DataRow(1, 2)]
		[DataRow(2, 2)]
		[DataRow(3, 2)]
		[DataRow(4, 2)]
		[DataRow(5, 3)]
		[DataRow(6, 2)]
		public void GetReservations(int siteId, int expectedOutput)
		{
			// Arrange
			ReservationDAL reservation = new ReservationDAL(ConnectionString);

			// Act
			var listOfReservations = reservation.GetReservations(new Site() { SiteId = siteId });

			// Assert
			Assert.AreEqual(expectedOutput, listOfReservations.Count);
		}

		[TestMethod]
		public void MakeReservation()
		{
			// Arrange
			ReservationDAL reservation = new ReservationDAL(ConnectionString);
			Reservation rsv = new Reservation
			{
				SiteId = 1,
				FromDate = new DateTime(2019,2,19),
				ToDate = new DateTime(2019,2,25),
				Name = "Testing"
			};

			// Act
			int newId = reservation.MakeReservation(rsv);

			// Assert
			int actualId = 0;
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					conn.Open();
					string query = $"Select Max(reservation_id) From reservation;";
					SqlCommand cmd = new SqlCommand(query, conn);
					actualId = Convert.ToInt32(cmd.ExecuteScalar());
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			Assert.AreEqual(actualId, newId);
		}
	}
}
