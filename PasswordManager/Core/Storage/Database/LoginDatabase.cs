using System;
using System.Collections.Generic;

namespace PasswordManager.Windows.Core.Storage.Database {
	[Serializable]
	public struct LoginDatabase {
		public List<LoginDatabaseRecord> Records;
	}
}