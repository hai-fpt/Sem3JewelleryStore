using System;
using MySql.Data.MySqlClient;
namespace JewelleryStore.Database
{
	public class MySqlConnectionManager
	{
		private readonly string _connectionString;
		public MySqlConnectionManager(string connectionString)
		{
			_connectionString = connectionString;
		}

		public MySqlConnection GetConnection()
		{
			MySqlConnection conn = new MySqlConnection(_connectionString);
			return conn;
		}
	}
}

