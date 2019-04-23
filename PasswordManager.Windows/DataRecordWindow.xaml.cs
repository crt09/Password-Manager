using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
			bool nameEmpty = BoxEmpty(NameBox);
			bool loginEmpty = BoxEmpty(LoginBox);
			bool passwordEmpty = BoxEmpty(PasswordBox);
			if (nameEmpty || loginEmpty || passwordEmpty) {
				return;
			}
			this.Cancelled = false;
			this.ServiceName = NameBox.Text;
			this.ServiceLogin = LoginBox.Text;
			this.ServicePassword = PasswordBox.Password;
			Close();
		}

		private bool BoxEmpty<TBox>(TBox box) where TBox : Control, new() {
			string content = string.Empty;
			if (box is TextBox textBox) {
				content = textBox.Text;
			} else if (box is PasswordBox passwordBox) {
				content = passwordBox.Password;
			} else {
				return true;
			}
			bool boxEmpty = (content == string.Empty);
			if (boxEmpty) {
				box.BorderBrush = Brushes.Red;
				WarningLabel.Content = "Please, enter all required data";
			}
			return boxEmpty;
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
			(sender as Control).BorderBrush = Brushes.Gray;
			WarningLabel.Content = string.Empty;
		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
			this.TextBox_TextChanged(sender, null);
		}
	}
}