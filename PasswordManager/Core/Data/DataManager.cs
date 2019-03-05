using System.Linq;
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
			for (int i = 0; i < database.Records.Count; i++) {
				database.Records[i].Key = (uint)i;
			}
			formatter.Serialize(database, path);
		}

		public LoginDatabase Load() {
			LoginDatabase database = formatter.Deserialize(path);
			return database;
		}

		public void DeleteByKey(uint key) {
			LoginDatabase database = Load();
			database.Records.RemoveAll(rec => rec.Key == key);
			Save(database);
		}

		public void EditByKey(uint key, LoginDatabaseRecord record) {
			LoginDatabase database = Load();
			for (int i = 0; i < database.Records.Count; i++) {
				if (database.Records[i].Key == key) {
					database.Records[i] = record;
					break;
				}
			}			
			Save(database);
		}
	}
}