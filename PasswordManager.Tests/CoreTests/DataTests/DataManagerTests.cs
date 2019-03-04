using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordManager.Windows.Core.Data;
using PasswordManager.Windows.Core.Storage.Database;

namespace PasswordManager.Tests.Core.DataTests {
	[TestClass]
	public class DataManagerTests {

		private string path = "database_test.dat";
		private DataManager manager;

		public DataManagerTests() {
			manager = new DataManager(path);
		}

		[TestMethod]
		public void SaveLoad_Add5Records_GetRecordsCountReturn5() {
			var database = new LoginDatabase();
			var record = new LoginDatabaseRecord();

			database.Records.Add(record);
			database.Records.Add(record);
			database.Records.Add(record);
			database.Records.Add(record);
			database.Records.Add(record);

			manager.Save(database);
			var deDatabase = manager.Load();

			var count = deDatabase.Records.Count;
			Assert.AreEqual(5, count);

			File.Delete(path);
		}

		[TestMethod]
		public void SaveLoad_Add2Records_2ndRecordNameMustBeNotEmpty() {
			var database = new LoginDatabase();
			var record = new LoginDatabaseRecord();

			database.Records.Add(record);
			record.Name = "test_name";
			database.Records.Add(record);

			manager.Save(database);
			var deDatabase = manager.Load();

			Assert.AreEqual(database.Records[1].Name, deDatabase.Records[1].Name);

			File.Delete(path);
		}
	}
}