using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Core
{
    public interface IProgramRepository
    {
        List<Program> FindProgram(DateTime startDate, DateTime endDate);
    }



    public class ProgramRepository : IProgramRepository
    {
        public List<Program> FindProgram(DateTime startDate, DateTime endDate)
        {
            using (IDbConnection connection =
               new MySql.Data.MySqlClient.MySqlConnection(
                   "Host=192.168.1.125;UserId=test;Password=test;Database=mythconverg;CharSet=utf8"))
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select channel.chanid, starttime, endtime, title, subtitle, description, category,  UNIX_TIMESTAMP(program.starttime) AS id,channel.name, channel.xmltvid from program inner join channel on program.chanid=channel.chanid where starttime >= '" + startDate + "' and endtime <= '" + endDate + "' order by starttime";
                    command.CommandType = CommandType.Text;
                    
                    using (var reader = command.ExecuteReader())
                    {
                        var programs = new List<Program>();
                        while (reader.Read())
                        {
                            programs.Add(new Program
                            {
                                ChanId = reader.GetString(0),
                                StartTime = reader.GetDateTime(1),
                                EndTime = reader.GetDateTime(2),
                                Title = Encoding.UTF8.GetString(Encoding.GetEncoding("iso-8859-1").GetBytes(reader.GetString(3))),
                                Subtitle = Encoding.UTF8.GetString(Encoding.GetEncoding("iso-8859-1").GetBytes(reader.GetString(4))),
                                Description = Encoding.UTF8.GetString(Encoding.GetEncoding("iso-8859-1").GetBytes(reader.GetString(5))),
                                Category = reader.GetString(6),
                                Id = reader.GetString(7),
                                ChannelName = reader.GetString(8),
                                XMLTV = reader.GetString(9)
                            });
                        }
                        return programs;
                    }
                }
            }
        }

        public List<Program> FindMovies(DateTime time, DateTime days)
        {
            var programs = FindProgram(time, days);
            var movies = from program in programs
                         where program.EndTime.Subtract(program.StartTime).TotalMinutes > 80
                         orderby program.StartTime
                         select program;
            return movies.ToList();
        }
    }
}