using KDentist.Model;
using KDentist.ViewModels.VideoViewModels;
using KDentist.Views.VIdeosViews;
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

namespace KDentist.Views.VideosViews
{
    /// <summary>
    /// Interaction logic for VideosHome.xaml
    /// </summary>
    public partial class VideosHome : Page
    {
        public VideosHome()
        {
            InitializeComponent();


        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as VideosHomeViewModel);

            var isValid = viewmodel.AddVideoViewModel.IsModelValid();

            if (isValid)
            {
                viewmodel.AddVideoViewModel.Add.Execute(null);
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

        private void UpdateVideo_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse((sender as Button).Tag.ToString());

            var page = new EditVideo();
            var context=(page.DataContext as EditVideoViewModel);
            context.LoadData(id);
            context.NotifyAll();
            NavigationService.Navigate(page);

        }

        private void RemoveVideo_Click(object sender, RoutedEventArgs e)
        {
            var Id = int.Parse((sender as Button).CommandParameter.ToString());
            (this.DataContext as VideosHomeViewModel).Delete.Execute(Id);
        }

        private void VideosListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 35; // take into account vertical scrollbar
            var col1 = 0.60;
            var col2 = 0.40;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
        }

        private void RedirectToVideo(object sender, SelectionChangedEventArgs e)
        {

            var url = ((sender as ListView).SelectedItem as Video).Url;

            var page = new ShowVideo(url);


            NavigationService.Navigate(page);
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            var context = (this.DataContext as VideosHomeViewModel);
            context.GetVideosFromDB();

            var word = (sender as TextBox).Text.ToString().Trim().ToLower().Replace(" ", "");

            var searchedBy = this.SearchBy.Text.ToString();

            var items = context.Videos;


            for (int i = 0; i < items.Count; i++)
            {
                if (searchedBy == "Име")
                {
                    if (!items[i].Name.ToLower().Contains(word))
                    {
                        context.Videos.Remove(items[i]);
                        i--;
                    }
                }
            }

            context.NotifyVideos();
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
