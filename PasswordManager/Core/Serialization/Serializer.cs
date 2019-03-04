using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PasswordManager.Windows.Core.Serialization {
	public class Serializer<TObj> where TObj : new() {

		private BinaryFormatter formatter;
	
		public Serializer() {
			formatter = new BinaryFormatter();
		}

		public void Serialize(TObj obj, string to) {
			using (var stream = new FileStream(to, FileMode.OpenOrCreate)) {
				formatter.Serialize(stream, obj);
			}
		}

		public TObj Deserialize(string from) {
			if(!File.Exists(from))
				this.Serialize(new TObj(), from);
			using (var stream = new FileStream(from, FileMode.OpenOrCreate)) {
				var obj = (TObj)formatter.Deserialize(stream);
				return obj;
			}
		}
	}
}