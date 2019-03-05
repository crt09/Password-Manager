using System.Windows;
using System.Windows.Controls;
using PasswordManager.Windows.Core.Storage.Database;
using PasswordManager.Windows.Models;

namespace PasswordManager.Windows.Views {
	public partial class MainView : UserControl {
		private DataModel dataModel;

		public MainView() {
			InitializeComponent();
			dataModel = new DataModel();
			this.UpdateDataPanel();
		}

		private void UpdateDataPanel() {
			DataPanel.Children.Clear();
			for (int i = 0; i < dataModel.Records.Count; i++) {
				var dataView = new DataRecordView((uint)i);				
				dataView.Key = dataModel.Records[i].Key;
				dataView.Name.Content = dataModel.Records[i].Name;
				dataView.Login.Text = dataModel.Records[i].Login;
				dataView.Password.Password = dataModel.Records[i].Password;
				dataView.Delete += DataViewOnDelete;
				dataView.Edit += DataViewOnEdit;
				DataPanel.Children.Add(dataView);
			}
		}

		private void DataViewOnEdit(uint key, LoginDatabaseRecord record) {
			dataModel.EditValue(key, record);
			this.UpdateDataPanel();
		}

		private void DataViewOnDelete(uint key) {
			dataModel.RemoveValue(key);
			this.UpdateDataPanel();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e) {
			var dataWindow = new DataRecordWindow();
			dataWindow.Title = "Add service";
			dataWindow.ActionButton.Content = "Add";
			dataWindow.ShowDialog();
			if (dataWindow.Cancelled) return;

			var record = new LoginDatabaseRecord();
			record.Name = dataWindow.ServiceName;
			record.Login = dataWindow.ServiceLogin;
			record.Password = dataWindow.ServicePassword;
			dataModel.AddValue(record);		
			this.UpdateDataPanel();
		}
	}
}