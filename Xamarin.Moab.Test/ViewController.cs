using Foundation;
using System;
using UIKit;
using Xamarin.Nuke;

namespace Xamarin.Moab.Test
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            var image = new UIImageView
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            Add(image);

            ImagePipeline.Shared.LoadImageWithUrl(
                new NSUrl("https://placekitten.com/g/300/300"),
                (img, url) => image.Image = img);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
