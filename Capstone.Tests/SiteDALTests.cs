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
	public class SiteDALTests :CampingTests
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
	}
}
