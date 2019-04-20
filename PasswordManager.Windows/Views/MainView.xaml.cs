using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PasswordManager.Models;
using PasswordManager.Windows.Core.Storage.Database;

namespace PasswordManager.Windows.Views {
	public partial class MainView : UserControl {

		private DataModel dataModel;
		private ConfigurationModel configModel;

		public MainView() {
			InitializeComponent();
			dataModel = new DataModel();
			configModel = new ConfigurationModel();
			this.UpdateDataPanel();

			var genView = new PasswordGenView();
			PasswordGenGrid.Children.Add(genView);
		}

		private void UpdateDataPanel() {
			DataPanel.Children.Clear();
			var records = dataModel.Records;
			for (int i = 0; i < records.Count; i++) {
				var dataView = CreateDataView(records[i]);
				dataView.UpdateIndexProperties((uint)i);
				DataPanel.Children.Add(dataView);
			}
		}

		private void UpdateDataPanelProperties() {
			for (int i = 0; i < DataPanel.Children.Count; i++) {
				if (DataPanel.Children[i] is DataRecordView dataView) {
					dataView.UpdateIndexProperties((uint)i);
					dataView.Key = (uint)i;
				}
			}
		}

		private DataRecordView CreateDataView(LoginDatabaseRecord record) {
			var dataView = new DataRecordView();
			dataView.Key = record.Key;
			dataView.ServiceName.Content = record.Name;
			dataView.ServiceLogin.Text = record.Login;
			dataView.ServicePassword.Password = record.Password;
			dataView.Delete += DataViewOnDelete;
			dataView.Edit += DataViewOnEdit;
			return dataView;
		}

		private void DataViewOnEdit(uint key, LoginDatabaseRecord record) {
			dataModel.EditValue(key, record);
			foreach (var element in DataPanel.Children) {
				if (element is DataRecordView dataView && dataView.Key == key) {
					dataView.ServiceName.Content = record.Name;
					dataView.ServiceLogin.Text = record.Login;
					dataView.ServicePassword.Password = record.Password;
					return;
				}
			}			
		}

		private void DataViewOnDelete(uint key) {
			dataModel.RemoveValue(key);
			foreach (var element in DataPanel.Children) {
				if (element is DataRecordView dataView) {
					if (dataView.Key == key) {
						DataPanel.Children.Remove(dataView);
						break;
					}
				}
			}
			this.UpdateDataPanelProperties();
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

			var view = CreateDataView(record);
			DataPanel.Children.Add(view);
			this.UpdateDataPanelProperties();
		}

		private void ChangePasswordButton_Click(object sender, RoutedEventArgs e) {
			if (!configModel.Login(OldAppPassword.Password)) {
				ShowInfo("Incorrect password", Brushes.Red);
				OldAppPassword.BorderBrush = Brushes.Red;
				return;
			}
			if(NewAppPassword.Password != NewAppPasswordRepeat.Password) {
				ShowInfo("Passwords aren't the same", Brushes.Red);
				NewAppPassword.BorderBrush = Brushes.Red;
				NewAppPasswordRepeat.BorderBrush = Brushes.Red;
				return;
			}
			if(NewAppPassword.Password == string.Empty) {
				ShowInfo("Please, enter all required data", Brushes.Red);
				NewAppPassword.BorderBrush = Brushes.Red;
				NewAppPasswordRepeat.BorderBrush = Brushes.Red;
				return;
			}

			configModel.Register(NewAppPassword.Password);
			OldAppPassword.Password = string.Empty;
			NewAppPassword.Password = string.Empty;
			NewAppPasswordRepeat.Password = string.Empty;

			InfoLabel.Foreground = Brushes.Green;
			InfoLabel.Content = "Password successfully changed";
		}

		private void ShowInfo(string text, Brush color) {
			InfoLabel.Content = text;
			InfoLabel.Foreground = color;
		}

		private void AppPassword_PasswordChanged(object sender, RoutedEventArgs e) {
			(sender as Control).BorderBrush = Brushes.Gray;
			InfoLabel.Content = string.Empty;
		}
	}
}