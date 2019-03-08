using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordManager.Windows.Core.Helpers;

namespace PasswordManager.Tests.CoreTests.SerializationTests {
	[TestClass]
	public class SerializerTests {

		[Serializable]
		struct SerializableStruct {
			public int TestInt { get; set; }
			public string TestString { get; set; }
		}

		string path = "testdata/storage_test.dat";

		[TestMethod]
		public void Deserialize_FromCurrentDirectoryAsObject_Pass() {
			var serializer = new Serializer<SerializableStruct>();
			var obj = new SerializableStruct();
			obj.TestInt = 41;
			obj.TestString = "test";

			try {
				obj = serializer.Deserialize(path);
			} catch (Exception e) {
				Assert.Fail(e.Message);
			}
			Assert.AreEqual(0, obj.TestInt);
			Assert.AreEqual(null, obj.TestString);

			File.Delete(path);
		}

		[TestMethod]
		public void SerializeAndDeserialize_StructToCurrentDirectory_Pass() {		
			var serializer = new Serializer<SerializableStruct>();
			var obj = new SerializableStruct();
			obj.TestInt = 13;
			obj.TestString = "devtest";

			try {
				serializer.Serialize(obj, path);
			} catch (Exception e) {
				Assert.Fail(e.Message);
			}

			var deObj = new SerializableStruct();
			try {
				deObj = serializer.Deserialize(path);
			} catch (Exception e) {
				Assert.Fail(e.Message);
			}

			Assert.AreEqual(obj.TestInt, deObj.TestInt);
			Assert.AreEqual(obj.TestString, deObj.TestString);

			File.Delete(path);
		}
	}
}
