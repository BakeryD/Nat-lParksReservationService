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
	public class CampgroundDALTests : CampingTests
	{
		[DataTestMethod]
		[DataRow(1, 2)]
		[DataRow(2, 1)]
		[DataRow(3, 1)]
		public void GetCampgrounds(int parkId, int expectedOutput)
		{
			// Arrange
			CampgroundDAL campground = new CampgroundDAL(ConnectionString);

			// Act
			var listOfCampgrounds = campground.GetCampgrounds(new Park() { ParkId = parkId });

			// Assert
			Assert.AreEqual(expectedOutput, listOfCampgrounds.Count);
		}
	}
}
