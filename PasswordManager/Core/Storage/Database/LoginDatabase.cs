using System;
using System.Collections.Generic;

namespace PasswordManager.Windows.Core.Storage.Database {
	[Serializable]
	public class LoginDatabase {
		public List<LoginDatabaseRecord> Records;

		public LoginDatabase() {
			Records = new List<LoginDatabaseRecord>();
		}

		public LoginDatabase Clone() {
			var records = new List<LoginDatabaseRecord>();
			foreach (var record in this.Records) {
				var dbRecord = new LoginDatabaseRecord();
				dbRecord.Key = record.Key;
				dbRecord.Name = record.Name;
				dbRecord.Login = record.Login;
				dbRecord.Password = record.Password;
				records.Add(dbRecord);
			}
			var database = new LoginDatabase();
			database.Records = records;
			return database;
		}
	}
}