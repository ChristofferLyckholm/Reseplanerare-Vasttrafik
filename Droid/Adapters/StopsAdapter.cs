
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace stops.Adapters
{
	
	public class StopsAdapter {
		public string Name { get; set; }
		public string Id { get; set; }


		public override string ToString ()
		{
			return Name;
		}
	}
}

