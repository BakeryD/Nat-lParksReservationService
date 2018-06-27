using System;
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
	}
}
