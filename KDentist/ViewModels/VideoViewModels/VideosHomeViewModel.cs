using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KDentist.ViewModels.VideoViewModels
{
    public class VideosHomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private AddVideoViewModel addVideoViewModel;

        public AddVideoViewModel AddVideoViewModel
        {
            get
            {
                return this.addVideoViewModel;
            }
            set
            {
                this.addVideoViewModel = value;
            }
        }

        private ObservableCollection<Video> videos;

        public ObservableCollection<Video> Videos
        {
            get
            {
                return this.videos;
            }
        }

        public VideosHomeViewModel()
        {
            GetVideosFromDB();
            this.addVideoViewModel = new AddVideoViewModel();
        }

        public void NotifyVideos()
        {
            NotifyPropertyChanged("Videos");
        }
        public void GetVideosFromDB()
        {
            var videosDb = this.videoService.GetAll()
               .Reverse();

            videos = new ObservableCollection<Video>(videosDb);
            NotifyVideos();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }


        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                if (this.delete == null)
                {
                    this.delete = new RelayCommand(this.DeleteC);
                }
                return this.delete;
            }
        }
        private void DeleteC(object obj)
        {
            MessageBoxResult question = MessageBox.Show("Искате ли да изтриете тoва видео?", "Изтриване", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Yes)
            {
                int id = int.Parse(obj.ToString());
                if (this.videoService.Exist(id))
                {
                    this.videoService.Delete(id);
                    Videos.Remove(Videos.Where(x => x.Id == id).FirstOrDefault());
                }
            } 
            NotifyVideos();
        }
    }
}
