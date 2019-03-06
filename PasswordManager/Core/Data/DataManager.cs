using PasswordManager.Windows.Core.Serialization;
using PasswordManager.Windows.Core.Storage.Database;

namespace PasswordManager.Windows.Core.Data {
	public class DataManager : IManager<LoginDatabase> {

		private Serializer<LoginDatabase> formatter;
		private readonly string path;
		private readonly string encryptionKey;

		public DataManager(string path) {
			formatter = new Serializer<LoginDatabase>();
			this.path = path;
			this.encryptionKey = "ASDPs=lS$Tsslkgj==";
		}		

		public void Save(LoginDatabase database) {
			var bufferDatabase = database.Clone();
			for (int i = 0; i < bufferDatabase.Records.Count; i++) {
				bufferDatabase.Records[i].Key = (uint)i;
				bufferDatabase.Records[i].Login = Encryption.Encrypt(bufferDatabase.Records[i].Login, encryptionKey);
				bufferDatabase.Records[i].Password = Encryption.Encrypt(bufferDatabase.Records[i].Password, encryptionKey);
			}
			formatter.Serialize(bufferDatabase, path);
		}

		public LoginDatabase Load() {
			LoginDatabase database = formatter.Deserialize(path);
			foreach (var record in database.Records) {
				record.Login = Encryption.Decrypt(record.Login, encryptionKey);
				record.Password = Encryption.Decrypt(record.Password, encryptionKey);
			}
			return database;
		}
	}
}