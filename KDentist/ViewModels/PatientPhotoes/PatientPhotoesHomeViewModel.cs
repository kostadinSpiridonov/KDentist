using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.ViewModels.PatientPhotoes
{
    public class PatientPhotoesHomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public void Clear()
        {
            photoes = new ObservableCollection<string>();
            NotifyPropertyChanged("Photoes");
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

        private ObservableCollection<string> photoes;

        public ObservableCollection<string> Photoes
        {
            get
            {
                return this.photoes;
            }
        }

        public bool AddImage(string url)
        {
            if (File.Exists(url))
            {
                try
                {
                    var guidName = Guid.NewGuid().ToString();
                    var photoType = url.Substring(url.LastIndexOf('.'));
                    var path = AppDomain.CurrentDomain.GetData("DataDirectory") + "/Photoes/" + id.ToString() + "/" + guidName + photoType;

                    File.Copy(url, path);


                    GetImages(this.id);
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            return false;
        }

        public void GetImages(int id)
        {
            var path = AppDomain.CurrentDomain.GetData("DataDirectory") + "/Photoes/" + id.ToString() + "/";
            DirectoryInfo di = new DirectoryInfo(path);
            FileSystemInfo[] files = di.GetFileSystemInfos();
            var orderedFiles = files.OrderByDescending(f => f.CreationTime).Select(x => x.FullName);
            photoes = new ObservableCollection<string>(orderedFiles.ToList());

            this.id = id;

            var patient = this.patientService.GetById(id);
            this.name = " " + patient.FirstName + " " + patient.SecondName + " " + patient.LastName;
            NotifyPropertyChanged("Photoes");
            NotifyPropertyChanged("Id");
            NotifyPropertyChanged("Name");
        }

        public bool Delete(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    photoes.Remove(photoes.Where(x => x == path).FirstOrDefault());
                    NotifyPropertyChanged("Photoes");
                    (new System.Threading.Thread(() =>
                    {
                        while (true)
                        {
                            try
                            {
                                File.Delete(path);
                                break;
                            }
                            catch { }
                        }
                    })).Start();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            return false;
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
