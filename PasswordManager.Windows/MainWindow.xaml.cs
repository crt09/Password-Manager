using System.Windows;
using PasswordManager.Models;
using PasswordManager.Windows.Views;

namespace PasswordManager.Windows {
	public partial class MainWindow : Window {
		private ConfigurationModel configModel;
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
		}

		private void Login(string password) {					
			if (configModel.Login(password)) {
				this.RunMainView();
			}
		}

		private void Register(string password) {			
			configModel.Register(password);
			this.RunMainView();
		}

		private void RunMainView() {
			var view = new MainView();
			MainGrid.Children.Clear();
			MainGrid.Children.Add(view);
		}
	}
}
