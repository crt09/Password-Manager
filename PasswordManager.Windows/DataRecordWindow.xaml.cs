using System.Windows;

namespace PasswordManager.Windows {
	public partial class DataRecordWindow : Window {

		public string ServiceName { get; set; }
		public string ServiceLogin { get; set; }
		public string ServicePassword { get; set; }
		public bool Cancelled { get; set; } = true;

		public DataRecordWindow() {
			InitializeComponent();
		}

		private void ActionButton_Click(object sender, RoutedEventArgs e) {
			if (NameBox.Text == string.Empty
			    || LoginBox.Text == string.Empty
			    || PasswordBox.Password == string.Empty)
				return;
			this.Cancelled = false;
			this.ServiceName = NameBox.Text;
			this.ServiceLogin = LoginBox.Text;
			this.ServicePassword = PasswordBox.Password;
			Close();
		}
	}
}