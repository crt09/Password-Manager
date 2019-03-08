using System.Collections.ObjectModel;
using System.Linq;
using PasswordManager.Windows.Core.Data;
using PasswordManager.Windows.Core.Serialization;
using PasswordManager.Windows.Core.Storage.Database;

namespace PasswordManager.Models {
	public class DataModel {

		private readonly string encryptionKey;

		public ReadOnlyCollection<LoginDatabaseRecord> Records {
			get {
				var records = dataManager.Load().Records;
				foreach (var record in records) {
					record.Login = Encryption.Decrypt(record.Login, this.encryptionKey);
					record.Password = Encryption.Decrypt(record.Password, this.encryptionKey);
				}
				return new ReadOnlyCollection<LoginDatabaseRecord>(records);
			}
		}
			

		private readonly DataManager dataManager;
		public DataModel() {
			dataManager = new DataManager("data/database.dat");	
			this.encryptionKey = "slpgnTnJ";
		}

		/// <summary>
		/// Add record to database and save it.
		/// </summary>
		/// <param name="record">Record to add</param>
		public void AddValue(LoginDatabaseRecord record) {
			var recordBuffer = record.Clone();
			recordBuffer.Login = Encryption.Encrypt(recordBuffer.Login, this.encryptionKey);
			recordBuffer.Password = Encryption.Encrypt(recordBuffer.Password, this.encryptionKey);
			LoginDatabase database = dataManager.Load();
			database.Records.Add(recordBuffer);
			dataManager.Save(database);			
		}

		/// <summary>
		/// Remove record from database by key and save it.
		/// </summary>
		/// <param name="key">Data record key. Keys are generating by order of values addition</param>
		public void RemoveValue(uint key) {
			LoginDatabase database = dataManager.Load();
			database.Records.RemoveAll(rec => rec.Key == key);
			dataManager.Save(database);
		}

		/// <summary>
		/// Replace record in database by key and save it.
		/// </summary>
		/// <param name="key">Key to find replacing record</param>
		/// <param name="record">Record to replace key record</param>
		public void EditValue(uint key, LoginDatabaseRecord record) {
			var recordBuffer = record.Clone();
			recordBuffer.Login = Encryption.Encrypt(recordBuffer.Login, this.encryptionKey);
			recordBuffer.Password = Encryption.Encrypt(recordBuffer.Password, this.encryptionKey);
			LoginDatabase database = dataManager.Load();
			for (int i = 0; i < database.Records.Count; i++) {
				if (database.Records[i].Key == key) {
					database.Records[i] = recordBuffer;
					break;
				}
			}
			dataManager.Save(database);
		}		
	}
}