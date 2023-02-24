using Foundation;
using System;
using UIKit;
using Xamarin.Moab;

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
                TranslatesAutoresizingMaskIntoConstraints = false,
            };

            Add(image);

            var image2 = new UIImageView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
            };

            Add(image2);

            var image3 = new UIImageView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
            };

            Add(image3);


            // 150 MB
            nint cacheLimit = 1024 * 1024 * 150;
            ImagePipeline.Shared.SetCacheStrategyWithCacheStrategy(DataCachingStrategy.DataCache, "xamarin.moab.test.cache", cacheLimit);

            ImagePipeline.Shared.LoadImageWithUrl(
                new NSUrl("https://placekitten.com/g/300/300"),
                (img, url) => image.Image = img);


            ImagePipeline.Shared.LoadImageWithUrl(
                new NSUrl("https://placekitten.com/g/600/600"),
                (img, url) => image2.Image = img);


            ImagePipeline.Shared.LoadImageWithUrl(
                new NSUrl("https://placekitten.com/g/1000/1000"),
                (img, url) => image3.Image = img);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
