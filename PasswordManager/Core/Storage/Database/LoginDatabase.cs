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
				records.Add(record.Clone());
			}
			var database = new LoginDatabase();
			database.Records = records;
			return database;
		}
	}
}