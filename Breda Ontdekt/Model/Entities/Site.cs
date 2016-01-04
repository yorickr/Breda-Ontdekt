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

        public Site(string id, string name, Geopoint location, string description) : base(name, location, id)
        {
            try
            {
                Debug.WriteLine(description);
                int descriptionId = Int32.Parse(description);
                switch(descriptionId)
                {
                    //nassaumonument
                    case 1:
                        base.description = GetDescription(new Uri("ms-appx:///Assets/text/nassaumonument.txt"));
                        break;
                    //kasteel
                    case 2:
                        base.description = GetDescription(new Uri("ms-appx:///Assets/text/kasteel.txt"));
                        break;
                    //torenstraat
                    case 3:
                        base.description = GetDescription(new Uri("ms-appx:///Assets/text/torenstraat.txt"));
                        break;
                    //stadhuis
                    case 4:
                        base.description = GetDescription(new Uri("ms-appx:///Assets/text/stadhuis.txt"));
                        break;
                    //antoniuskerk
                    case 5:
                        base.description = GetDescription(new Uri("ms-appx:///Assets/text/antoniuskerk.txt"));
                        break;
                    //bibliotheek
                    case 6:
                        base.description = GetDescription(new Uri("ms-appx:///Assets/text/bibliotheek.txt"));
                        break;
                    //kloosterkazerne
                    case 7:
                        base.description = GetDescription(new Uri("ms-appx:///Assets/text/kloosterkazerne.txt"));
                        break;
                }
                Debug.WriteLine("Succesfully converted description to id.");
            }
            catch(Exception)
            {
                Debug.WriteLine("Couldn't succesfully convert description to id");
                base.description = description;
            }

            this.images = new List<BitmapImage>();
        }

        private string GetDescription(Uri uri)
        {
            StorageFile file = StorageFile.GetFileFromApplicationUriAsync(uri).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
            return File.ReadAllText(file.Path);
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
