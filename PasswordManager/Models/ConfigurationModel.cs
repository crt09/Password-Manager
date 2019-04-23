using PasswordManager.Windows.Core.Helpers;
using PasswordManager.Windows.Core.Managers;
using PasswordManager.Windows.Core.Storage;

namespace PasswordManager.Models {
	public class ConfigurationModel {

		public static string ConfigPath => "data/config.dat";
		public string AppPassword => configManager.Load().AppPassword;
		public bool Registered => !configManager.IsConfigMissing();
		private readonly string encryptionKey;

		private readonly ConfigurationManager configManager;	
		public ConfigurationModel() {
			configManager = new ConfigurationManager(ConfigPath);
			this.encryptionKey = "jYshfjkc";
		}

		/// <summary>
		/// Save argument password to configuration file.
		/// </summary>
		/// <param name="password">Password to save</param>
		public void Register(string password) {
			ConfigStorage storage = configManager.Load();
			storage.AppPassword = Encryption.Encrypt(password, this.encryptionKey);
			configManager.Save(storage);
		}

		/// <summary>
		/// Password validation.
		/// Check if argument password is same as password from configuration file.
		/// </summary>
		/// <param name="password">Password to check</param>
		/// <returns>Validation result</returns>
		public bool Login(string password) {
			ConfigStorage storage = configManager.Load();
			string decryptedPassword = Encryption.Decrypt(storage.AppPassword, this.encryptionKey);
			return decryptedPassword == password;
		}

		public void SaveGenProperties(uint minLength, uint maxLength, string pattern) {
			ConfigStorage storage = configManager.Load();
			storage.GenMinLength = minLength;
			storage.GenMaxLength = maxLength;
			storage.GenPattern = pattern;
			configManager.Save(storage);
		}

		public ConfigStorage LoadGenProperties() {
			ConfigStorage storage = configManager.Load();
			storage.AppPassword = null;
			return storage;
		}

		public void ResetGenProperties() {
			var defaultStorage = new ConfigStorage();
			ConfigStorage storage = configManager.Load();
			storage.GenMinLength = defaultStorage.GenMinLength;
			storage.GenMaxLength = defaultStorage.GenMaxLength;
			storage.GenPattern = defaultStorage.GenPattern;
			configManager.Save(storage);
		}
	}
}