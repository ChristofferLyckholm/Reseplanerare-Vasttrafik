using System;
using System.Xml.Serialization;

namespace Core.Interfaces
{
	public interface iFileSystem
	{
		void serilaze(XmlSerializer xml, string path, object list);
		object deserilaze(XmlSerializer xmlSerializer, string path);
		bool exist (string fileName);

	}
}





