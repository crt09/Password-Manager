using PasswordManager.Windows.Core.Serialization;
using PasswordManager.Windows.Core.Storage.Database;

namespace PasswordManager.Windows.Core.Data {
	public class DataManager : IManager<LoginDatabase> {

		private Serializer<LoginDatabase> formatter;
		private readonly string path;

		public DataManager(string path) {
			formatter = new Serializer<LoginDatabase>();
			this.path = path;
		}		

		public void Save(LoginDatabase database) {
			var bufferDatabase = database.Clone();
			for (int i = 0; i < bufferDatabase.Records.Count; i++) {
				bufferDatabase.Records[i].Key = (uint)i;
			}
			formatter.Serialize(bufferDatabase, path);
		}

		public LoginDatabase Load() {
			LoginDatabase database = formatter.Deserialize(path);
			return database;
		}
	}
}