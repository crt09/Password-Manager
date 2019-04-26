using PasswordManager.Models;
using PasswordManager.Windows.Views;
using System.Windows;

namespace PasswordManager.Windows {
	public partial class MainWindow : Window {
		private readonly ConfigurationModel configModel;
		public MainWindow() {
			InitializeComponent();
			configModel = new ConfigurationModel();

			var loginView = new AppLoginView();
			if (configModel.Registered) {
				loginView.InfoLabel.Content = "Enter password";
				loginView.ActionButton.Content = "Login";
				loginView.ActionButtonClick += Login;
			} else {
				loginView.InfoLabel.Content = "Register";
				loginView.ActionButton.Content = "Save";
				loginView.RepeatPassword.Visibility = Visibility.Visible;
				loginView.ActionButtonClick += Register;
			}

			MainGrid.Children.Add(loginView);
			loginView.Password.Focus();
		}

		private bool Login(string password) {
			bool loginSuccess = configModel.Login(password);
			if (loginSuccess) {
				this.RunMainView();
			}
			return loginSuccess;
		}

		private bool Register(string password) {
			configModel.Register(password);
			this.RunMainView();
			return true;
		}

		private void RunMainView() {
			Dispatcher.Invoke(() => {
				MainGrid.Children.Clear();
				var view = new MainView();
				MainGrid.Children.Add(view);
			});		
		}
	}
}
