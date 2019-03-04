using System.Collections.Generic;
using PasswordManager.Windows.Core.Serialization;
using PasswordManager.Windows.Core.Storage.Database;

namespace PasswordManager.Windows.Core.Data {
	public class DataManager : IManager<LoginDatabase> {

		private Serializer<LoginDatabase> formatter;
		private string path;

		public DataManager(string path) {
			formatter = new Serializer<LoginDatabase>();
			this.path = path;
		}		

		public void Save(LoginDatabase database) {
			formatter.Serialize(database, path);
		}

		public LoginDatabase Load() {
			var database = formatter.Deserialize(path);
			return database;
		}

		public LoginDatabase CreateDatabase() {
			var database = new LoginDatabase();
			database.Records = new List<LoginDatabaseRecord>();
			return database;
		}
	}
}