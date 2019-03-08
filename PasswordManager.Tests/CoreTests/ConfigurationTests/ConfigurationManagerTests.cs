using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordManager.Windows.Core.Managers;
using PasswordManager.Windows.Core.Storage;

namespace PasswordManager.Tests.CoreTests.ConfigurationTests {
	[TestClass]
	public class ConfigurationManagerTests {

		private string path = "test_config.dat";
		private ConfigurationManager manager;

		public ConfigurationManagerTests() {
			manager = new ConfigurationManager(path);
		}

		[TestMethod]
		public void IsMissing_MissingFile_returnTrue() {
			if(File.Exists(path))
				File.Delete(path);
			bool missing = manager.IsConfigMissing();
			Assert.AreEqual(true, missing);
		}

		[TestMethod]
		public void IsMissing_HasFile_returnFalse() {
			manager.Save(new ConfigStorage());
			bool missing = manager.IsConfigMissing();
			Assert.AreEqual(false, missing);
			File.Delete(path);
		}

		[TestMethod]
		public void IsMissing_HasWrongFile_returnTrue() {
			var stream = File.Create(path);
			stream.Close();
			bool missing = manager.IsConfigMissing();
			Assert.AreEqual(true, missing);
			File.Delete(path);
		}

		[TestMethod]
		public void SaveLoad_SaveAndLoadConfig_ConfigsAreEqual() {
			var storage = new ConfigStorage();
			storage.AppPassword = "4563";
			var deStorage = new ConfigStorage();

			manager.Save(storage);
			deStorage = manager.Load();

			Assert.AreEqual(storage.AppPassword, deStorage.AppPassword);

			File.Delete(path);
		}
	}
}