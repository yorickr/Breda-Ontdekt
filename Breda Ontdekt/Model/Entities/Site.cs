using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Breda_Ontdekt.Model.Entities
{
    public class Site : ObjectInfo
    {
        public List<BitmapImage> images { get; set; }

        public Site(string name, Geoposition location, string description) : base(name, location)
        {
            base.description = description;
            this.images = new List<BitmapImage>();
        }

        public void AddImage(Uri url)
        {
            BitmapImage image = new BitmapImage(url);
            AddImage(image);
        }

        public void AddImage(BitmapImage image)
        {
            images.Add(image);
        }

  
    }
}
