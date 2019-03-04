namespace PasswordManager.Windows.Core {
	public interface IManager<T> {
		void Save(T storage);
		T Load();
	}
}