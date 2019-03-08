using System;

namespace PasswordManager.Windows.Core.Storage.Database {
	[Serializable]
	public class LoginDatabaseRecord {
		public uint Key { get; set; }
		public string Name { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }

		public LoginDatabaseRecord Clone() {
			var buffer = new LoginDatabaseRecord();
			buffer.Key = this.Key;
			buffer.Name = this.Name;
			buffer.Login = this.Login;
			buffer.Password = this.Password;
			return buffer;
		}
	}
}