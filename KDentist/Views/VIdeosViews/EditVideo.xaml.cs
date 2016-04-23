using KDentist.ViewModels.VideoViewModels;
using KDentist.Views.VideosViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KDentist.Views.VIdeosViews
{
    /// <summary>
    /// Interaction logic for EditVideo.xaml
    /// </summary>
    public partial class EditVideo : Page
    {
        public EditVideo()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new VideosHome());
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as EditVideoViewModel);

            var isValid = viewmodel.IsModelValid();

            if (isValid)
            {
                viewmodel.Save.Execute(null);
                NavigationService.Navigate(new VideosHome());
            }
            else
            {
                if (this.Name.Text == null || this.Name.Text == "")
                {
                    this.Name.Text = "";
                }

                if (this.Url.Text == null || this.Url.Text == "")
                {
                    this.Url.Text = "";
                }
            }
        }


        private void LoadVideo(object sender, TextChangedEventArgs e)
        {

            LoadEror.Visibility = Visibility.Collapsed;
            if (Url.Text != "")
            {
                var sb = new StringBuilder();

                Match regexMatch = Regex.Match(this.Url.Text, "^[^v]+v=(.{11}).*",
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
                    sb.Append("       <iframe style=\"width: 98% !important;height: 78% !important;\" type=\"text/html\" class=\"embed-responsive-item\" src=\"http://www.youtube.com/embed/" + code + "\" frameborder=\"0\"></iframe>");
                    sb.Append("    </body>");
                    sb.Append("</html>");

                    wbr.NavigateToString(sb.ToString());
                }
                else
                {
                    LoadEror.Visibility = Visibility.Visible;
                }
            }
            else
            {
                LoadEror.Visibility = Visibility.Visible;
            }
        }
    }
}
