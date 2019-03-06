using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PasswordManager.Windows.Core.Storage.Database;

namespace PasswordManager.Windows.Views {
	public partial class DataRecordView : UserControl {

		public event Action<uint> Delete;
		public event Action<uint, LoginDatabaseRecord> Edit;

		public uint Key { get; set; }

		public DataRecordView(uint index) {
			InitializeComponent();
			if (index % 2 > 0) Background = Brushes.WhiteSmoke;
			if (index == 0) Separator.Visibility = Visibility.Collapsed;
		}

		private void ShowPasswordCheckbox_Checked(object sender, RoutedEventArgs e) {
			PasswordTextBox.Visibility = Visibility.Visible;
			ServicePassword.Visibility = Visibility.Collapsed;
			PasswordTextBox.Text = ServicePassword.Password;
		}

		private void ShowPasswordCheckbox_OnUnchecked(object sender, RoutedEventArgs e) {
			PasswordTextBox.Visibility = Visibility.Collapsed;
			ServicePassword.Visibility = Visibility.Visible;
		}

		private void Login_OnPreviewMouseDown(object sender, MouseButtonEventArgs e) {
			Clipboard.SetText(ServiceLogin.Text);
		}

		private void Password_OnPreviewMouseDown(object sender, MouseButtonEventArgs e) {
			Clipboard.SetText(ServicePassword.Password);
		}

		private void Password_OnPreviewTextInput(object sender, TextCompositionEventArgs e) {
			e.Handled = true;
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e) {
			var result = MessageBox.Show("Are you sure?", "Delete service", MessageBoxButton.YesNo, MessageBoxImage.None);
			if (result == MessageBoxResult.No) return;
			Delete?.Invoke(this.Key);
		}

		private void EditButton_Click(object sender, RoutedEventArgs e) {
			var dataWindow = new DataRecordWindow();
			dataWindow.Title = "Edit service";
			dataWindow.ActionButton.Content = "Save";
			dataWindow.NameBox.Text = this.ServiceName.Content.ToString();
			dataWindow.LoginBox.Text = this.ServiceLogin.Text;
			dataWindow.PasswordBox.Password = this.ServicePassword.Password;
			dataWindow.ShowDialog();
			if(dataWindow.Cancelled) return;

			var record = new LoginDatabaseRecord();
			record.Name = dataWindow.ServiceName;
			record.Login = dataWindow.ServiceLogin;
			record.Password = dataWindow.ServicePassword;
			Edit?.Invoke(this.Key, record);
		}
	}
}