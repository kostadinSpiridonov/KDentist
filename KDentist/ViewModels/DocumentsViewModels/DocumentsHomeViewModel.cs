using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.ViewModels.DocumentsViewModel
{
    public class DocumentsHomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public void Clear()
        {
            documents = new ObservableCollection<DocumentViewModel>();
            NotifyPropertyChanged("Documents");
        }

        private ObservableCollection<DocumentViewModel> documents;

        public ObservableCollection<DocumentViewModel> Documents
        {
            get
            {
                return this.documents;
            }
        }

        public bool AddDocument(string url)
        {
            if (File.Exists(url))
            {
                try
                {
                    var guidName = url.Substring(url.LastIndexOf("\\"));
                    var path = AppDomain.CurrentDomain.GetData("DataDirectory") + "/Documents" + guidName;
                    if(File.Exists(path))
                    {
                        var r = path.Substring(0, path.LastIndexOf("."));
                        var ex=path.Substring(path.LastIndexOf("."));
                        r += Guid.NewGuid();
                        r+=ex;
                        path = r;
                    }
                    File.Copy(url, path);


                    GetDocuments();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            return false;
        }

        public void GetDocuments()
        {
            var path = AppDomain.CurrentDomain.GetData("DataDirectory") + "/Documents/";
            DirectoryInfo di = new DirectoryInfo(path);
            FileSystemInfo[] files = di.GetFileSystemInfos();
            var orderedFiles = files.OrderByDescending(f => f.CreationTime).Select(x => x.FullName);
            documents = new ObservableCollection<DocumentViewModel>(orderedFiles.ToList()
                .ConvertAll(x=>new DocumentViewModel
                {
                    Url=x,
                    Name=x.Substring( x.LastIndexOf(@"\")+1)
                }));

            NotifyPropertyChanged("Documents");
        }

        public bool Delete(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    documents.Remove(documents.Where(x => x.Url == path).FirstOrDefault());
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
    public class DocumentViewModel
    {
        public string Url { get; set; }

        public string Name { get; set; }
    }
}
