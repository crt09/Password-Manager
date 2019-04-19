using PasswordManager.Models;
using PasswordManager.Windows.Core.Storage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManager.Windows.Views {
	public partial class PasswordGenView : UserControl {
		private readonly PasswordGenModel genModel;
		private readonly ConfigurationModel configModel;
		public PasswordGenView() {
			InitializeComponent();
			genModel = new PasswordGenModel();
			configModel = new ConfigurationModel();
			this.LoadConfig();
		}

		private void PasswordBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e) {
			Clipboard.SetText(PasswordBox.Text);
		}

		private void GenerateButton_OnClick(object sender, RoutedEventArgs e) {
			this.Generate();
		}

		private void Generate() {
			PasswordBox.Text = genModel.Generate((uint)MinLengthSlider.Value, (uint)MaxLengthSlider.Value, PatternTextBox.Text);
		}

		private void LengthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			if (MinLengthSlider == null || MaxLengthSlider == null)
				return;
			if (MaxLengthSlider.Value < MinLengthSlider.Value) {
				MaxLengthSlider.Value = MinLengthSlider.Value;
			}
			this.SaveConfig();
		}

		private void PatternTextBox_TextChanged(object sender, TextChangedEventArgs e) {
			this.SaveConfig();
		}

		private void SaveConfig() {
			if (configModel != null) {
				configModel.SaveGenProperties((uint)MinLengthSlider.Value, (uint)MaxLengthSlider.Value, PatternTextBox.Text);
			}
		}

		private void LoadConfig() {
			ConfigStorage config = configModel.LoadGenProperties();
			MinLengthSlider.Value = config.GenMinLength;
			MaxLengthSlider.Value = config.GenMaxLength;
			PatternTextBox.Text = config.GenPattern;
			this.Generate();
		}

		private void ResetButton_Click(object sender, RoutedEventArgs e) {
			configModel.ResetGenProperties();
			this.LoadConfig();
		}
	}
}