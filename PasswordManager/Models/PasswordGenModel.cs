using System;

namespace PasswordManager.Models {
	public class PasswordGenModel {
		private Random randomGen;

		public string Generate(uint minLength, uint maxLength, string pattern) {
			var random = randomGen ?? (randomGen = new Random());
			string result = string.Empty;
			int randomLength = random.Next((int)minLength, (int)maxLength + 1);
			for (uint i = 0; i < randomLength; i++) {
				int randomIndex = random.Next(0, pattern.Length);
				result += pattern[randomIndex];
			}
			return result;
		}
	}
}