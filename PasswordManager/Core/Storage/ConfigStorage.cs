using System;

namespace PasswordManager.Windows.Core.Storage {
	[Serializable]
	public class ConfigStorage {
		public string AppPassword { get; set; }

		public ConfigStorage Clone() {
			var storage = new ConfigStorage();
			storage.AppPassword = this.AppPassword;
			return storage;
		}
	}
}