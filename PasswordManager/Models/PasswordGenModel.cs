using System;
using PasswordManager.Windows.Core.Serialization;

namespace PasswordManager.Models {
	public class PasswordGenModel {
		public string Generate() {
			long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			string text = timestamp.ToString();
			string key = text.Substring(text.Length - 8, 8);
			string encrypted = Encryption.Encrypt(text, key);

			int length = new Random().Next(12, encrypted.Length - 2);
			string password = encrypted.Substring(0, length);
			return password;
		}
	}
}