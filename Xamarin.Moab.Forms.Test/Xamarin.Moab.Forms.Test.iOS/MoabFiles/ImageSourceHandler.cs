﻿using System.Threading;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Moab.Forms.Test.iOS.MoabFiles;

[assembly: ExportImageSourceHandler(typeof(FileImageSource), typeof(ImageSourceHandler))]
[assembly: ExportImageSourceHandler(typeof(UriImageSource), typeof(ImageSourceHandler))]
namespace Xamarin.Moab.Forms.Test.iOS.MoabFiles
{
    [Preserve(AllMembers = true)]
    public class ImageSourceHandler : IImageSourceHandler, IAnimationSourceHandler
    {
        internal static readonly FileImageSourceHandler DefaultFileImageSourceHandler = new FileImageSourceHandler();

        private static readonly ImageLoaderSourceHandler DefaultUriImageSourceHandler = new ImageLoaderSourceHandler();

        public Task<UIImage> LoadImageAsync(
            ImageSource imageSource,
            CancellationToken cancellationToken = new CancellationToken(),
            float scale = 1) =>
            MoabHelper.LoadViaNuke(imageSource, cancellationToken, scale);

        public Task<FormsCAKeyFrameAnimation> LoadImageAnimationAsync(
            ImageSource imageSource,
            CancellationToken cancellationToken = new CancellationToken(),
            float scale = 1)
        {
            FormsHandler.Debug(() => $"Delegating animation of {imageSource} to default Xamarin.Forms handler");

            if (imageSource is UriImageSource)
            {
                return DefaultUriImageSourceHandler.LoadImageAnimationAsync(imageSource, cancellationToken, scale);
            }

            return DefaultFileImageSourceHandler.LoadImageAnimationAsync(imageSource, cancellationToken, scale);
        }
    }
}

