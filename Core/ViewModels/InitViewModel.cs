using System;
using Core.Interfaces;
using Ninject;
using System.Threading.Tasks;
using Core.Http;
using Core.Util;

namespace Core.ViewModels
{
	public class InitViewModel : EventClass
	{	
		private IPlatform _platform;
		[Inject]
		public InitViewModel(iFileSystem fileSystem, IPlatform platform)
		{
            Injected.Instance.Filesystem = fileSystem;
            Injected.Instance.Platform = platform;
			_platform = platform;

		}




	}
}

