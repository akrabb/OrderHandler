using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OrderHandler.Helpers {
	public class XmlHelper : IXmlHelper {
		public bool NewLineOnAttributes { get; set; }

		public string ToXml(object obj, XmlSerializerNamespaces xmlSerializerNamespaces) {
			Type type = obj.GetType();

			var xmlSerializer = new XmlSerializer(type);
			var xmlWriterSettings = new XmlWriterSettings {
				Indent = true,
				NewLineOnAttributes = NewLineOnAttributes,
				OmitXmlDeclaration = true
			};

			var stringBuilder = new StringBuilder();
			using(XmlWriter writer = XmlWriter.Create(stringBuilder, xmlWriterSettings)) {
				xmlSerializer.Serialize(writer, obj, xmlSerializerNamespaces);
			}
			return stringBuilder.ToString();
		}

		public string ToXml(object obj) {
			var xmlSerializerNamespaces = new XmlSerializerNamespaces();
			xmlSerializerNamespaces.Add("", "");
			return ToXml(obj, xmlSerializerNamespaces);
		}

		public T FromXml<T>(string xml) {
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			using(StringReader stringReader = new StringReader(xml)) {
				return (T)xmlSerializer.Deserialize(stringReader);
			}
		}

		public object FromXml(string xml, string typeName) {
			Type type = Type.GetType(typeName);
			XmlSerializer xmlSerializer = new XmlSerializer(type);
			using(StringReader stringReader = new StringReader(xml)) {
				return xmlSerializer.Deserialize(stringReader);
			}
		}

		public void ToXmlFile(Object obj, string filePath) {
			var xmlSerializer = new XmlSerializer(obj.GetType());
			var xmlSerializerNamespaces = new XmlSerializerNamespaces();
			var xmlWriterSettings = new XmlWriterSettings {
				Indent = true,
				NewLineOnAttributes = NewLineOnAttributes,
				OmitXmlDeclaration = true
			};
			xmlSerializerNamespaces.Add("", "");

			using(XmlWriter writer = XmlWriter.Create(filePath, xmlWriterSettings)) {
				xmlSerializer.Serialize(writer, obj);
			}
		}

		public T FromXmlFile<T>(string filePath) {
			StreamReader streamReader = new StreamReader(filePath);
			try {
				var result = FromXml<T>(streamReader.ReadToEnd());
				return result;
			} catch(Exception) {
				return default(T);
			} finally {
				streamReader.Close();
			}
		}
	}
}