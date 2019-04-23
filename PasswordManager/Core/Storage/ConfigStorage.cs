using System;

namespace PasswordManager.Windows.Core.Storage {
	[Serializable]
	public class ConfigStorage {
		public string AppPassword { get; set; }
		public uint GenMinLength { get; set; } = 8;
		public uint GenMaxLength { get; set; } = 12;
		public string GenPattern { get; set; } = @"!#$%&*0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

		public ConfigStorage Clone() {
			var storage = new ConfigStorage();
			storage.AppPassword = this.AppPassword;
			storage.GenMinLength = this.GenMinLength;
			storage.GenMaxLength = this.GenMaxLength;
			storage.GenPattern = this.GenPattern;
			return storage;
		}
	}
}