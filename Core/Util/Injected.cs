using System;
using Core.Interfaces;

namespace Core.Util
{
    public class Injected
    {
        private static Injected instance;

        private Injected() { }

        public iFileSystem Filesystem;
        public IPlatform Platform;

        public static Injected Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Injected();
                }
                return instance;
            }
        }
    }
}
