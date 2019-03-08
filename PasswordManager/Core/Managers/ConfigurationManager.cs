using PasswordManager.Windows.Core.Helpers;
using PasswordManager.Windows.Core.Storage;

namespace PasswordManager.Windows.Core.Managers {
	public class ConfigurationManager : IManager<ConfigStorage> {

		private Serializer<ConfigStorage> formatter;
		private readonly string path;		

		public ConfigurationManager(string path) {
			formatter = new Serializer<ConfigStorage>();
			this.path = path;			
		}

		public bool IsConfigMissing() {
			return formatter.DataMissing(path);
		}

		public void Save(ConfigStorage storage) {
			var bufferStorage = storage.Clone();			
			formatter.Serialize(bufferStorage, path);
		}

		public ConfigStorage Load() {
			var storage = formatter.Deserialize(path);			
			return storage;
		}
	}
}