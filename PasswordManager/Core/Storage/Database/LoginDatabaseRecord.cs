using System;

namespace PasswordManager.Windows.Core.Storage.Database {
	[Serializable]
	public class LoginDatabaseRecord {
		public uint Key { get; set; }
		public string Name { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
	}
}