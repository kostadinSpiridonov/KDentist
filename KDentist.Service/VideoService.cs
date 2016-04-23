using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Service
{
    public interface IVideoService
    {
        void Add(Video model);
        ICollection<Video> GetAll();
        void Delete(int id);
        void Update(Video model);
        bool Exist(int id);
        Video GetById(int id);
    }

    public class VideoService:BaseService,IVideoService
    {
        public void Add(Video model)
        {
            this.context.Videos.Add(model);
            this.context.SaveChanges();
        }

        public ICollection<Video> GetAll()
        {
            return this.context.Videos.ToList();
        }

        public void Delete(int id)
        {
            var video=this.context.Videos.Find(id);
            this.context.Videos.Remove(video);
            this.context.SaveChanges();
        }

        public void Update(Video model)
        {
            var video = this.context.Videos.Find(model.Id);
            video.Name = model.Name;
            video.Url = model.Url;
            this.context.SaveChanges();
        }

        public Video GetById(int id)
        {
            return this.context.Videos.Find(id);
        }


        public bool Exist(int id)
        {
            return this.context.Videos.Any(x => x.Id == id);
        }
    }
}
