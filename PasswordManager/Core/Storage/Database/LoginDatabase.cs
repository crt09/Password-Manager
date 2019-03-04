using System;
using System.Collections.Generic;

namespace PasswordManager.Windows.Core.Storage.Database {
	[Serializable]
	public class LoginDatabase {
		public List<LoginDatabaseRecord> Records = new List<LoginDatabaseRecord>();
	}
}