using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace iOS
{
    public partial class ViewController : UIViewController
    {
        private double _lineLeft = 0;
        private double _lineRight = 0;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            /*
             * set up Line
             */
            _lineLeft = _line.Frame.X;
            _lineRight = _line.Frame.X + 116;

            /*
             * Add page 1
             */
            NSArray array = NSBundle.MainBundle.LoadNib("Slide1", this, null);
            UIView slide1 = Runtime.GetNSObject(array.ValueAt(0)) as UIView;
            slide1.Frame = new CoreGraphics.CGRect(0, 0, this.View.Frame.Width, scrollview.Frame.Height);
            scrollview.AddSubview(slide1);

            /*
            * Add page 2
            */
            NSArray array2 = NSBundle.MainBundle.LoadNib("Slide2", this, null);
            UIView slide2 = Runtime.GetNSObject(array2.ValueAt(0)) as UIView;
            slide2.Frame = new CoreGraphics.CGRect(this.View.Frame.Width, 0, this.View.Frame.Width, scrollview.Frame.Height);
            scrollview.AddSubview(slide2);

            /*
             * Change scrollview contentsize
             */ 
            scrollview.ContentSize = new CoreGraphics.CGSize(this.View.Frame.Width *2, scrollview.Frame.Height);

            /*
             * Handle the viewpager
             */

            this.scrollview.Scrolled += (sender, e) =>
            {
               moveLine(scrollview.ContentOffset.X / this.View.Frame.Width);

                /*
                 * page 1 - adjust scrollview height
                 */ 
                if(scrollview.ContentOffset.X == 0) {
                    scrollview.Frame = new CoreGraphics.CGRect(scrollview.Frame.X, scrollview.Frame.Y, this.View.Frame.Width, 200);
                }
                /*
                 * page 2
                 */ 
                else if(scrollview.ContentOffset.X > 1 && scrollview.Frame.Height != this.View.Frame.Height - 150) {
                    scrollview.Frame = new CoreGraphics.CGRect(scrollview.Frame.X, scrollview.Frame.Y, this.View.Frame.Width, this.View.Frame.Height-150);
                }
            };
             
        }


        private void moveLine(double procent) {
            _line.Frame = new CoreGraphics.CGRect(_lineLeft + (procent * _lineRight), _line.Frame.Y, _line.Frame.Width, _line.Frame.Height);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
