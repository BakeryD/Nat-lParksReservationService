using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    class CampgroundDAL
    {
		private readonly string ConnectionString;

		/// <summary>
		/// Initializes the DAL class with the connection string for our database.
		/// </summary>
		/// <param name="connectString">Connection string to database.</param>
		public CampgroundDAL(string connectString)
		{
			ConnectionString = connectString;
		}



	}
}
