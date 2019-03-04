using System.IO;
using PasswordManager.Windows.Core.Serialization;
using PasswordManager.Windows.Core.Storage;

namespace PasswordManager.Windows.Core.Configuration {
	public class ConfigurationManager : IManager<ConfigStorage> {

		private Serializer<ConfigStorage> formatter;
		private string path;

		public ConfigurationManager(string path) {
			formatter = new Serializer<ConfigStorage>();
			this.path = path;
		}

		public bool IsConfigMissing() {
			if (!File.Exists(path)) return true;
			try {
				formatter.Deserialize(path);
			} catch {
				return true;
			}
			return false;
		}

		public void Save(ConfigStorage storage) {
			formatter.Serialize(storage, path);
		}

		public ConfigStorage Load() {
			var storage = formatter.Deserialize(path);
			return storage;
		}
	}
}