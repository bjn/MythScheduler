using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Core
{
    public class ChannelIconService
    {
        private static Dictionary<string, ImageSource> _images;

        static ChannelIconService()
        {
            _images = new Dictionary<string, ImageSource>();
        }

        public ImageSource Get(string xmltv)
        {
            if (_images.ContainsKey(xmltv))
                return _images[xmltv];

            string url = string.Format("http://kroppkakor.dyndns.org/mythweb/data/tv_icons/{0}.png", xmltv);

            Stream stream = new MemoryStream(new WebClient().DownloadData(url));
            _images.Add(xmltv, new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
                                                      BitmapCacheOption.Default).Frames[0]);
            return _images[xmltv];
        }

        public ImageSource Get(ChannelId channelId)
        {
            if (_images.ContainsKey(channelId.Id))
                return _images[channelId.Id];

            string url = string.Format("http://kroppkakor.dyndns.org:6540/Myth/GetChannelIcon?ChanId={0}", channelId.Id);

            Stream stream = new MemoryStream(new WebClient().DownloadData(url));
            _images.Add(channelId.Id, new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
                                                      BitmapCacheOption.Default).Frames[0]);
            return _images[channelId.Id];
        }
    }
}