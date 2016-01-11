using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Media;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Breda_Ontdekt.Model.Entities
{
    public class Site : ObjectInfo
    {
        public List<BitmapImage> images { get; set; }

        public Site(string id, string name, Geopoint location, string language) : base(name, location, id)
        {
            try
            {
                int descriptionID = Int32.Parse(id);

                if (language == "NL")
                {
                    base.description = GetDescription(new Uri("ms-appx:///Assets/text/" + descriptionID + "NL.txt"));
                }
                else if (language == "EN")
                {
                    base.description = GetDescription(new Uri("ms-appx:///Assets/text/" + descriptionID + "EN.txt"));
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Couldn't succesfully convert description to id");
                base.description = description;
            }

            this.images = new List<BitmapImage>();
        }

        public void enableGeofencing()
        {
            base.isGeofencePoint = true;
        }

        private string GetDescription(Uri uri)
        {
            StorageFile file = StorageFile.GetFileFromApplicationUriAsync(uri).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
            return File.ReadAllText(file.Path,Encoding.UTF7);
        }

        public override string ToString()
        {
            return base.name + " " + base.position.Position.ToString() + " " + description;
        }

        public Geopoint GetPoint()
        {
            return base.position;
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
