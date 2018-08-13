using System;
using Core.Interfaces;
using Ninject.Modules;
using iOS.Implementations;
using Ninject;


namespace iOS.Setup
{
    public static class IoC
    {
        public static StandardKernel Container { get; set; }
    }

    public class NinjectLoader : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IPlatform>().To<Platform>();
            this.Bind<iFileSystem>().To<FileSystem>();
            //this.Bind<iToast>().To<Toast>();
        }
    }
}
