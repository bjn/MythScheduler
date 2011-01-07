using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core;

namespace MythScheduler
{
    public class ProgramViewModel
    {
        public ObservableCollection<Channel> Channels { get; set; }
    }

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            var channels = new MythXMLProgramRepository().GetChannels(DateTime.Now, DateTime.Now.AddDays(1));
            DataContext = new ProgramViewModel
                              {
                                  Channels = new ObservableCollection<Channel>(channels)
                              };
//            DataContext = new ProgramViewModel{Programs = new ObservableCollection<Program>(new ProgramRepository().FindMovies(DateTime.Now, DateTime.Now.AddDays(20)))};
        }

        private List<Channel> _channels;
        public List<Channel> Channels { 
            get
        {
            if (_channels == null)
            {
                _channels = new ChannelRepository().Find(DateTime.Now, DateTime.Now.AddDays(20));
            }
            return _channels;
        } }

        private void OnHyperlinkClick(object sender, RoutedEventArgs e)
        {
            Process.Start((e.Source as Hyperlink).NavigateUri.ToString());
        }
    }
}
