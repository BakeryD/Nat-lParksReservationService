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
	public class ParkDALTests : CampingTests
	{
		[TestMethod]
		public void GetParks()
		{
			// Arrange
			ParkDAL park = new ParkDAL(ConnectionString);

			// Act
			var listOfParks = park.GetParks();

			// Assert
			Assert.AreEqual(3, listOfParks.Count);
		}
	}
}
