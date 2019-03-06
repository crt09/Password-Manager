using PasswordManager.Windows.Core.Serialization;
using PasswordManager.Windows.Core.Storage;

namespace PasswordManager.Windows.Core.Configuration {
	public class ConfigurationManager : IManager<ConfigStorage> {

		private Serializer<ConfigStorage> formatter;
		private readonly string path;
		private readonly string encryptionKey;

		public ConfigurationManager(string path) {
			formatter = new Serializer<ConfigStorage>();
			this.path = path;
			this.encryptionKey = "SoMBnsrgoJSfogjfkfk=";
		}

		public bool IsConfigMissing() {
			return formatter.DataMissing(path);
		}

		public void Save(ConfigStorage storage) {
			var bufferStorage = storage.Clone();
			bufferStorage.AppPassword = Encryption.Encrypt(bufferStorage.AppPassword, encryptionKey);
			formatter.Serialize(bufferStorage, path);
		}

		public ConfigStorage Load() {
			var storage = formatter.Deserialize(path);
			storage.AppPassword = Encryption.Decrypt(storage.AppPassword, encryptionKey);
			return storage;
		}
	}
}