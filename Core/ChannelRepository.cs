using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Core
{
    public class ChannelRepository
    {
        public List<Channel> Find(DateTime startDate, DateTime endTime)
        {
            var programs = new ProgramRepository().FindProgram(startDate, endTime);

            //foreach (var channel in Channels)
            //{
            //    channel.Programs = (from program in programs
            //                        where program.ChanId == channel.Id && program.EndTime.Subtract(program.StartTime).TotalMinutes > 80
            //                        select program).ToList();
            //}

            return Channels;
        }

        private static List<Channel> Channels
        {
            get
            {
                if (_channels == null)
                {
                    _channels = LoadChannels();
                }

                return _channels;
            }
        }

        private static List<Channel> LoadChannels()
        {
            using (IDbConnection connection =
                new MySql.Data.MySqlClient.MySqlConnection(
                    "Host=192.168.1.125;UserId=test;Password=test;database=mythconverg"))
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select chanid, name, icon from channel order by name";
                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        var channels = new List<Channel>();
                        //while (reader.Read())
                        //{
                        //    channels.Add(new Channel
                        //                     {
                        //                         Id = reader.GetString(0),
                        //                         Name = reader.GetString(1),
                        //                         Icon = reader.GetString(2)
                        //                     });
                        //}
                        return channels;
                    }
                }
            }


        }

        private static List<Channel> _channels;
    }
}