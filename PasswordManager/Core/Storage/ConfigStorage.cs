using System;

namespace PasswordManager.Windows.Core.Storage {
	[Serializable]
	public struct ConfigStorage {
		public string AppPin { get; set; }
	}
}