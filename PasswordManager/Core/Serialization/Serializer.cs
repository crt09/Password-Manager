using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PasswordManager.Windows.Core.Serialization {
	public class Serializer<TObj> where TObj : new() {

		private BinaryFormatter formatter;
	
		public Serializer() {
			formatter = new BinaryFormatter();
		}

		public void Serialize(TObj obj, string to) {
			var path = Path.GetDirectoryName(to);
			if (path != string.Empty && !Directory.Exists(path))
				Directory.CreateDirectory(path);
			if(File.Exists(to))
				File.Delete(to);
			using (var stream = new FileStream(to, FileMode.OpenOrCreate)) {
				formatter.Serialize(stream, obj);
			}
		}

		public TObj Deserialize(string from) {
			if (DataMissing(from))
				this.Serialize(new TObj(), from);
			using (var stream = new FileStream(from, FileMode.OpenOrCreate)) {				
				var obj = (TObj)formatter.Deserialize(stream);
				return obj;
			}
		}

		public bool DataMissing(string path) {
			try {
				using (var stream = new FileStream(path, FileMode.Open)) {
					formatter.Deserialize(stream);
				}
			} catch {
				return true;
			}
			return false;
		}
	}
}