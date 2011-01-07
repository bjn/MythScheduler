using System.Collections.Generic;
using System.Windows.Media;

namespace Core
{
    public class ChannelId
    {
        public ChannelId(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }

    public class Channel
    {
        public ChannelId Id { get; set; }
        public string Name { get; set; }
        public ImageSource Icon
        {
            get { return new ChannelIconService().Get(Id); }
        }

        public IEnumerable<Day> Days { get; set; }
       // public IEnumerable<Program> Programs { get; set; }
    }
}