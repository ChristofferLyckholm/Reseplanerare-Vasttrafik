using System;

namespace Core.Http
{
	public class HttpResult
	{

		public HttpResult(bool status, object obj) {
			Result = status;
			Data = obj;
		}

		public bool Result { get; set; }
		public object Data { get; set; }
	}
}

