using System;

namespace PasswordManager.Windows.Core.Storage.Database {
	[Serializable]
	public struct LoginDatabaseRecord {
		public string Name { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
	}
}