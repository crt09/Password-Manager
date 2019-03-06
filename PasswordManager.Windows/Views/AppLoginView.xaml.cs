using System;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager.Windows.Views {
	public partial class AppLoginView : UserControl {
		public event Action<string> ActionButtonClick;

		public AppLoginView() {
			InitializeComponent();
		}

		private void ActionButton_OnClick(object sender, RoutedEventArgs e) {
			if ((RepeatPassword.Visibility == Visibility.Visible && Password.Password == RepeatPassword.Password && Password.Password != string.Empty)
				|| (RepeatPassword.Visibility == Visibility.Collapsed && Password.Password != string.Empty)) {
				ActionButtonClick?.Invoke(Password.Password);
			}			
		}
	}
}
