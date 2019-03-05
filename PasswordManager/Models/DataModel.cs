using System.Collections.ObjectModel;
using PasswordManager.Windows.Core.Data;
using PasswordManager.Windows.Core.Storage.Database;

namespace PasswordManager.Models {
	public class DataModel {

		public ReadOnlyCollection<LoginDatabaseRecord> Records => 
			new ReadOnlyCollection<LoginDatabaseRecord>(dataManager.Load().Records);

		private readonly DataManager dataManager;
		public DataModel() {
			dataManager = new DataManager("database.dat");	
		}

		/// <summary>
		/// Add record to database and save it.
		/// </summary>
		/// <param name="record">Record to add</param>
		public void AddValue(LoginDatabaseRecord record) {
			LoginDatabase database = dataManager.Load();
			database.Records.Add(record);
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
			LoginDatabase database = dataManager.Load();
			for (int i = 0; i < database.Records.Count; i++) {
				if (database.Records[i].Key == key) {
					database.Records[i] = record;
					break;
				}
			}
			dataManager.Save(database);
		}		
	}
}