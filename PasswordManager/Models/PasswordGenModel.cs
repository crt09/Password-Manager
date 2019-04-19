using System;

namespace PasswordManager.Models {
	public class PasswordGenModel {
		public string Generate(uint minLength, uint maxLength, string pattern) {
			var symbols = pattern.ToCharArray();
			var random = new Random();
			string result = string.Empty;
			int randomLength = random.Next((int)minLength, (int)maxLength + 1);
			for (uint i = 0; i < randomLength; i++) {
				int randomIndex = random.Next(0, symbols.Length);
				result += symbols[randomIndex];
			}
			return result;
		}
	}
}