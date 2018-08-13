using System;
using System.Threading.Tasks;

namespace Core.Util
{
	public delegate void ClassEventHandler (object source, EventArgs e);

	public class EventClassArgs : EventArgs
	{
		public string Name;
		public object Data;
		public EventClassArgs(string name, object data)
		{
			Name = name;
			Data = data;
		}
	}
		
	public class EventClass
	{
		public event ClassEventHandler OnEvent;


		public async void Trigger(string name, int delay = 0) {
			await Trigger (name, null, delay);
		}

        public async Task<int> Trigger(string name, object data, int delay = 0) {

			if(delay>0) {
				await Task.Delay (delay);
				Trigger (name, data);
                return 1;
			}


			//To make sure we only trigger the event if a handler is present
			//we check the event to make sure it's not null.
			if(OnEvent != null)
			{
				OnEvent(this, new EventClassArgs(name, data));
			}

            return 1;

		}
	}
}

