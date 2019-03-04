using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordManager.Windows.Core.Storage;

namespace PasswordManager.Windows.Core.Configuration {
	[TestClass]
	public class ConfigurationManagerTests {

		private string path = "test_config.dat";

		[TestMethod]
		public void IsMissing_MissingFile_returnTrue() {
			if(File.Exists(path))
				File.Delete(path);
			bool missing = ConfigurationManager.IsMissing(path);
			Assert.AreEqual(true, missing);
		}

		[TestMethod]
		public void IsMissing_HasFile_returnFalse() {
			ConfigurationManager.Save(new ConfigStorage(), path);
			bool missing = ConfigurationManager.IsMissing(path);
			Assert.AreEqual(false, missing);
			File.Delete(path);
		}

		[TestMethod]
		public void IsMissing_HasWrongFile_returnTrue() {
			var stream = File.Create(path);
			stream.Close();
			bool missing = ConfigurationManager.IsMissing(path);
			Assert.AreEqual(true, missing);
			File.Delete(path);
		}

		[TestMethod]
		public void SaveLoad_SaveAndLoadConfig_ConfigsAreEqual() {
			var storage = new ConfigStorage();
			storage.AppPin = "4563";
			var deStorage = new ConfigStorage();

			ConfigurationManager.Save(storage, path);
			deStorage = ConfigurationManager.Load(path);

			Assert.AreEqual(storage.AppPin, deStorage.AppPin);

			File.Delete(path);
		}
	}
}