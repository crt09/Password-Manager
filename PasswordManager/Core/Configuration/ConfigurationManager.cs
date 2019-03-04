using System.IO;
using PasswordManager.Windows.Core.Serialization;
using PasswordManager.Windows.Core.Storage;

namespace PasswordManager.Windows.Core.Configuration {
	public static class ConfigurationManager {
		private static Serializer<ConfigStorage> formatter;
		static ConfigurationManager() {
			formatter = new Serializer<ConfigStorage>();
		}

		public static bool IsMissing(string path) {
			if (!File.Exists(path)) return true;
			try {
				formatter.Deserialize(path);
			} catch {
				return true;
			}
			return false;
		}

		public static void Save(ConfigStorage storage, string to) {
			formatter.Serialize(storage, to);
		}

		public static ConfigStorage Load(string from) {
			var storage = formatter.Deserialize(from);
			return storage;
		}
	}
}