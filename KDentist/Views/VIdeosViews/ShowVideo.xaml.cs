using KDentist.Views.VideosViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KDentist.Views.VIdeosViews
{
    /// <summary>
    /// Interaction logic for ShowVideo.xaml
    /// </summary>
    public partial class ShowVideo : Page
    {
        public ShowVideo(string url)
        {
            InitializeComponent();
            if (url != "")
            {
                var sb = new StringBuilder();

                Match regexMatch = Regex.Match(url, "^[^v]+v=(.{11}).*",
                         RegexOptions.IgnoreCase);
                if (regexMatch.Success)
                {
                    var id = regexMatch.Groups[1];

                    var code = id.Value.ToString();

                    sb.Append("<html>");
                    sb.Append("    <head>");
                    sb.Append("        <meta>");
                    sb.Append("    </head>");
                    sb.Append("    <body  style=\"overflow-y: hidden;\" background-color:pink>");
                    sb.Append("       <iframe style=\"width: 98% !important;height: 98% !important;\" type=\"text/html\" class=\"embed-responsive-item\" src=\"http://www.youtube.com/embed/" + code + "\" frameborder=\"0\"></iframe>");
                    sb.Append("    </body>");
                    sb.Append("</html>");

                    wbr.NavigateToString(sb.ToString());

                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (wbr != null)
            {
                    wbr.Navigate("about:blank");
            }
            NavigationService.Navigate(new VideosHome());
        }



    }
}
