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

		[TestInitialize]
		public void Initialize() {
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

		[TestMethod]
		public void DeleteByKeyTest() {
			var database = new LoginDatabase();

			var record1 = new LoginDatabaseRecord();
			record1.Name = "name1";
			database.Records.Add(record1);
			var record2 = new LoginDatabaseRecord();
			record2.Name = "name2";
			database.Records.Add(record2);
			manager.Save(database);

			manager.DeleteByKey(0);

			var deDatabase = manager.Load();
			Assert.AreEqual(1, deDatabase.Records.Count);
			Assert.AreEqual("name2", deDatabase.Records[0].Name);

			File.Delete(path);
		}

		[TestMethod]
		public void EditByKeyTest() {
			var database = new LoginDatabase();

			var record1 = new LoginDatabaseRecord();
			record1.Name = "name1";
			database.Records.Add(record1);
			var record2 = new LoginDatabaseRecord();
			record2.Name = "name2";
			database.Records.Add(record2);
			manager.Save(database);

			var editedRecord = new LoginDatabaseRecord();
			editedRecord.Name = "new_name";
			manager.EditByKey(1, editedRecord);

			var deDatabase = manager.Load();
			Assert.AreEqual(2, deDatabase.Records.Count);
			Assert.AreEqual("new_name", deDatabase.Records[1].Name);

			File.Delete(path);
		}
	}
}