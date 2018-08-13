using System;
using System.Xml.Serialization;

namespace Core.Interfaces
{
	public interface iToast
	{
		void show (string text, int delay = 0);
	}
}





