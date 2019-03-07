using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PasswordManager.Models;

namespace PasswordManager.Windows.Views {
	public partial class PasswordGenView : UserControl {
		private PasswordGenModel genModel;
		public PasswordGenView() {
			InitializeComponent();
			genModel = new PasswordGenModel();
			this.Generate();
		}

		private void PasswordBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e) {
			Clipboard.SetText(PasswordBox.Text);
		}

		private void GenerateButton_OnClick(object sender, RoutedEventArgs e) {
			this.Generate();
		}

		private void Generate() {
			PasswordBox.Text = genModel.Generate();
		}
	}
}