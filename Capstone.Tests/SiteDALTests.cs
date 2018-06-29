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
	public class SiteDALTests : CampingTests
	{
		[DataTestMethod]
		[DataRow(1, 2)]
		[DataRow(2, 1)]
		[DataRow(3, 1)]
		[DataRow(4, 2)]
		public void GetSites(int campgroundId, int expectedOutput)
		{
			// Arrange
			SiteDAL site = new SiteDAL(ConnectionString);

			// Act
			var listOfSites = site.GetSites(new Campground() { CampgroundId = campgroundId });

			// Assert
			Assert.AreEqual(expectedOutput, listOfSites.Count);
		}

		[DataTestMethod]
		[DataRow(1, 2)]
		[DataRow(2, 0)]
		[DataRow(3, 1)]
		[DataRow(4, 0)]
		public void FindAvailableSites(int campgroundID, int expectedOutput)
		{
			// Arrange
			SiteDAL site = new SiteDAL(ConnectionString);

			// Act
			var listOfReservations = site.FindAvailableSites(new DateTime(2018, 4, 25), new DateTime(2018, 6, 19), new Campground() { CampgroundId = campgroundID });

			// Assert
			Assert.AreEqual(expectedOutput, listOfReservations.Count);
		}

		[DataTestMethod]
		[DataRow(1, 0)]
		[DataRow(2, 0)]
		[DataRow(3, 0)]
		[DataRow(4, 2)]
		public void FindAvailableSitesAdvanced(int campgroundID, int expectedOutput)
		{
			// Arrange
			SiteDAL site = new SiteDAL(ConnectionString);

			// Act
			var listOfReservations = site.FindAvailableSitesAdvanced(new DateTime(2019, 6, 10), new DateTime(2019, 6, 15), new Campground() { CampgroundId = campgroundID }, 6, true, 0, false);

			// Assert
			Assert.AreEqual(expectedOutput, listOfReservations.Count);
		}
	}
}
