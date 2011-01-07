using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Core
{
    public class MythXMLProgramRepository
    {
        public IEnumerable<Channel> GetChannels(DateTime startDate, DateTime endDate)
        {
            string httpQuery = string.Format("http://{0}:{1}/Myth/GetProgramGuide?Pin={2}&StartTime={3}&EndTime={4}&NumOfChannels=1", "192.168.1.125", "6545", "0000", startDate.ToString("yyyy-MM-ddTHH:mm"), endDate.ToString("yyyy-MM-ddTHH:mm"));
            XDocument doc = XDocument.Load(httpQuery);
         
            return from channel in doc.Descendants("Channel")
                           let programs = from program in channel.Descendants("Program")
                                          select new Program
                                                     {
                                                         Title = program.Attribute("title").Value,
                                                         StartTime = DateTime.Parse(program.Attribute("startTime").Value)
                                                     }
                           select new Channel
                                      {
                                          Id = new ChannelId(channel.Attribute("chanId").Value),
                                          Name = channel.Attribute("channelName").Value,
                                          Days = from p in programs 
                                                 orderby p.StartTime 
                                                 group p by p.StartTime.Date into dayGroup 
                                                 select new Day
                                                        {
                                                            Date = dayGroup.Key,
                                                            Programs = dayGroup.ToList()
                                                        }
                                      };
        }
    }
}