using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public static class FileHelper
{
	public static void WriteToFile<T> (string filename, T item)
	{
		var serializer = new XmlSerializer (typeof (T));
		using (var stream = File.OpenWrite (filename))
		{
			serializer.Serialize(stream, item);
		}
	}
	
	public static T ReadFromFile<T> (string filename) where T : class
	{
		var serializer = new XmlSerializer (typeof (T));
		using (var xreader = XmlReader.Create(File.OpenRead(filename)))
		{
			T item = serializer.Deserialize(xreader) as T;
			return item;
		}
	}
}