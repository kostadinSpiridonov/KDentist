using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels.VideoViewModels
{
    public class EditVideoViewModel : BaseViewModel,INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
         
        private int id;

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }


        private string url;

        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (this.save == null)
                {
                    this.save = new RelayCommand(this.SaveC);
                }
                return this.save;
            }
        }

        private void SaveC(object obj)
        {
            var video = new Video
            {
                Id=this.id,
                Name = this.name,
                Url = this.url
            };

            this.videoService.Update(video);
        }

        public void LoadData(int id)
        {
            if (this.videoService.Exist(id))
            {
                var video = this.videoService.GetById(id);

                this.id = video.Id;
                this.name = video.Name;
                this.url = video.Url;
                NotifyAll();
            }
        }

        public void NotifyAll()
        {
            NotifyPropertyChanged("Id");
            NotifyPropertyChanged("Url");
            NotifyPropertyChanged("Name");
        }

        public bool IsModelValid()
        {
            var canExecute = true;

            if (String.IsNullOrEmpty(this.name))
            {
                canExecute = false;
            }
            if (String.IsNullOrEmpty(this.url))
            {
                canExecute = false;
            }
            Match regexMatch = Regex.Match(this.Url, "^[^v]+v=(.{11}).*",
                         RegexOptions.IgnoreCase);
            if (!regexMatch.Success)
            {
                canExecute = false;
            }
            return canExecute;
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
    }
}
