using System;

namespace OrderHandler.Helpers {
	public interface IXmlHelper {
		void ToXmlFile(Object obj, string filePath);
		T FromXmlFile<T>(string filePath);
	}
}
