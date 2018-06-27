using System;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
	[TestClass]
	public class CampingTests
	{
		public const string ConnectionString = @"Data Source=.\SQLExpress;Initial Catalog=Campground;Integrated Security=True";
		private TransactionScope transaction;

		[TestInitialize]
		public void SetUpData()
		{
			// Begin transaction
			transaction = new TransactionScope();

			// Read sql statements from file
			string sql = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "ParksDBTests.sql"));

			// Run statements
			using (SqlConnection conn = new SqlConnection(ConnectionString))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.ExecuteNonQuery();
			}
		}

		[TestCleanup]
		public void CleanUpData()
		{
			transaction.Dispose();
		}
	}
}
