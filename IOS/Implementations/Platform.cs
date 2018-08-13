using Core.Interfaces;
using UIKit;

namespace iOS.Implementations
{


    public class Platform : IPlatform
    {



        public string GetId()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.ToString();
        }

    }

}

