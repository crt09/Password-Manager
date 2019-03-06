﻿using PasswordManager.Windows.Core.Configuration;
using PasswordManager.Windows.Core.Storage;

namespace PasswordManager.Models {
	public class ConfigurationModel {

		public string AppPassword => configManager.Load().AppPassword;
		public bool Registered => !configManager.IsConfigMissing();

		private readonly ConfigurationManager configManager;	
		public ConfigurationModel() {
			configManager = new ConfigurationManager("data/config.dat");
		}

		/// <summary>
		/// Save argument password to configuration file.
		/// </summary>
		/// <param name="password">Password to save</param>
		public void Register(string password) {
			var storage = new ConfigStorage();
			storage.AppPassword = password;
			configManager.Save(storage);
		}

		/// <summary>
		/// Password validation.
		/// Check if argument password is same as password from configuration file.
		/// </summary>
		/// <param name="password">Password to check</param>
		/// <returns>Validation result</returns>
		public bool Login(string password) {
			var storage = configManager.Load();
			return storage.AppPassword == password;
		}
	}
}