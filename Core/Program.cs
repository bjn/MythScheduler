using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Core
{
    public class Day
    {
        public DateTime Date { get; set; }
        public IEnumerable<Program> Programs { get; set; }
    }

    public class Program
    {
        public string Description { get; set; }
        public string Subtitle { get; set; }
        public string Url
        {
            get
            {
                return string.Format("http://kroppkakor.dyndns.org/mythweb/tv/detail/{0}/{1}", ChanId, Id);
            }
        }

        public ImageSource ChannelIcon
        {
            get { return new ChannelIconService().Get(XMLTV); }
        }
        public string ChanId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Id { get; set; }

        public string ChannelName { get; set; }

        public string XMLTV { get; set; }
    }
}