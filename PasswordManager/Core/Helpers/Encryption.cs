using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Windows.Core.Helpers {
	public static class Encryption {

		public static string Encrypt(string text, string key) {
			if (text == null || text == string.Empty) return text;
			byte[] messageBytes = Encoding.UTF8.GetBytes(text);
			byte[] passwordBytes = Encoding.UTF8.GetBytes(key);

			DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
			ICryptoTransform transform = provider.CreateEncryptor(passwordBytes, passwordBytes);
			CryptoStreamMode mode = CryptoStreamMode.Write;

			MemoryStream memStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
			cryptoStream.Write(messageBytes, 0, messageBytes.Length);
			cryptoStream.FlushFinalBlock();

			byte[] encryptedMessageBytes = new byte[memStream.Length];
			memStream.Position = 0;
			memStream.Read(encryptedMessageBytes, 0, encryptedMessageBytes.Length);

			string encryptedMessage = Convert.ToBase64String(encryptedMessageBytes);
			return encryptedMessage;
		}

		public static string Decrypt(string text, string key) {
			if (text == null || text == string.Empty) return text;
			byte[] encryptedMessageBytes = Convert.FromBase64String(text);
			byte[] passwordBytes = Encoding.UTF8.GetBytes(key);

			DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
			ICryptoTransform transform = provider.CreateDecryptor(passwordBytes, passwordBytes);
			CryptoStreamMode mode = CryptoStreamMode.Write;

			MemoryStream memStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
			cryptoStream.Write(encryptedMessageBytes, 0, encryptedMessageBytes.Length);
			cryptoStream.FlushFinalBlock();

			byte[] decryptedMessageBytes = new byte[memStream.Length];
			memStream.Position = 0;
			memStream.Read(decryptedMessageBytes, 0, decryptedMessageBytes.Length);

			string message = Encoding.UTF8.GetString(decryptedMessageBytes);
			return message;
		}
	}
}