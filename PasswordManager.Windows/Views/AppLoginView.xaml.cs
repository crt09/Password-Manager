using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PasswordManager.Windows.Views {
	public partial class AppLoginView : UserControl {
		public event Func<string, bool> ActionButtonClick;

		public AppLoginView() {
			InitializeComponent();
		}

		private void ActionButton_OnClick(object sender, RoutedEventArgs e) {
			if(RepeatPassword.Visibility == Visibility.Visible) {
				if(Password.Password == string.Empty) {
					ShowInfo("Please, enter all required data");
					return;
				}
				if(Password.Password != RepeatPassword.Password) {
					ShowInfo("Passwords aren't the same");
					RepeatPassword.BorderBrush = Brushes.Red;
					return;
				}
				TryActionInvoke();
			} else {
				if(Password.Password == string.Empty) {
					ShowInfo("Please, enter all required data");
					return;
				}
				if (!TryActionInvoke()) {
					ShowInfo("Incorrect password");
					return;
				}
			}			
		}

		private void ShowInfo(string text) {
			WarningLabel.Content = text;
			Password.BorderBrush = Brushes.Red;
		}

		private void PasswordChanged(object sender, RoutedEventArgs e) {
			(sender as Control).BorderBrush = Brushes.Gray;
			WarningLabel.Content = string.Empty;
			if(RepeatPassword.Visibility != Visibility.Visible) {
				TryActionInvoke();
			}
		}

		private bool TryActionInvoke() {
			return ActionButtonClick?.Invoke(Password.Password) ?? false;
		}
	}
}
