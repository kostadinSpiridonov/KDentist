using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KDentist.ViewModels.VideoViewModels
{
    public class AddVideoViewModel:BaseViewModel
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

        private ICommand add;
        public ICommand Add
        {
            get
            {
                if (this.add == null)
                {
                    this.add = new RelayCommand(this.AddC);
                }
                return this.add;
            }
        }

        private void AddC(object obj)
        {
            var video = new Video
            {
                Name = this.name,
                Url = this.url
            };

            this.videoService.Add(video);
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
            if(!regexMatch.Success)
            {
                canExecute = false;
            }
            return canExecute;
        }
    }
}
