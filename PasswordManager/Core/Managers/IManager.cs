namespace PasswordManager.Windows.Core.Managers {
	public interface IManager<TData> {
		void Save(TData storage);
		TData Load();
	}
}