using System.Windows;
using PasswordManager.Windows.Views;

namespace PasswordManager.Windows {
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();			

			var view = new MainView();
			MainGrid.Children.Add(view);
		}
	}
}
