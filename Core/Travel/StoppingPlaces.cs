using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Core.Interfaces;

namespace Core.Travel
{
	public class StoppingPlaces
	{
		private List<StoppingPlace> _stopsList;
		private string _filePath;
		public StoppingPlaces (string filePath = null)
		{
			
			_stopsList = new List<StoppingPlace> ();
			_filePath = filePath;
		}



		public List<StoppingPlace> Load(iFileSystem filesystem) {

			if (_filePath == null) {
                return null;
			}
				
			XmlSerializer serializer = new XmlSerializer(typeof(List<StoppingPlace>));
			List<StoppingPlace> items = (List<StoppingPlace>)filesystem.deserilaze (serializer, _filePath);

			if (items != null) {
				_stopsList = items;
				return _stopsList;
			}
			return null;

		
		
		}

		public void Add(StoppingPlace stop) {
			_stopsList.Add(stop);
		}

		public void Clear() {
			_stopsList.Clear ();
		}

		public List<StoppingPlace> GetList() {
			return _stopsList;	
		}

		public void Save(iFileSystem filesystem) {

			if (_filePath == null) {
                return; 
			}

			List<StoppingPlace> yourlist = _stopsList;
			XmlSerializer serializer = new XmlSerializer(typeof(List<StoppingPlace>));
			filesystem.serilaze (serializer, _filePath, yourlist);

		}


			
	}
}

