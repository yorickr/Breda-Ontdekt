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

        public Site(string id, string name, Geopoint location, string description, string language) : base(name, location, id)
        {
            try
            {
                Debug.WriteLine(description);
                int descriptionId = Int32.Parse(description);
                switch (descriptionId)
                {
                    //nassaumonument
                    case 1:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/nassaumonumentNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/nassaumonumentEN.txt"));
                        }
                        break;
                    //kasteel
                    case 2:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/kasteelNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/kasteelEN.txt"));
                        }
                        break;
                    //torenstraat
                    case 3:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/torenstraatNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/torenstraatEN.txt"));
                        }
                        break;
                    //stadhuis
                    case 4:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/stadhuisNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/stadhuisEN.txt"));
                        }
                        break;
                    //antoniuskerk
                    case 5:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/antoniuskerkNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/antoniuskerkEN.txt"));
                        }
                        break;
                    //bibliotheek
                    case 6:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/bibliotheekNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/bibliotheekEN.txt"));
                        }
                        break;
                    //kloosterkazerne
                    case 7:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/kloosterkazerneNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/kloosterkazerneEN.txt"));
                        }
                        break;
                    //liefdezuster
                    case 8:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/liefdezusterNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/liefdezusterEN.txt"));
                        }
                        break;
                    //lighthouse
                    case 9:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/lighthouseNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/lighthouseEN.txt"));
                        }
                        break;
                    //stadhouderspoort
                    case 10:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/stadhouderspoortNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/stadhouderspoortEN.txt"));
                        }
                        break;
                     //huisvanbrecht
                    case 11:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/huisvanbrechtNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/huisvanbrechtEN.txt"));
                        }
                        break;
                    //Spanjaardsgat
                    case 12:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/spanjaardsgatNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/spanjaardsgatEN.txt"));
                        }
                        break;
                    //Grote Kerk
                    case 13:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/grotekerkNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/grotekerkEN.txt"));
                        }
                        break;
                    //bevrijdingsmonument
                    case 14:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/bevrijdingsmonumentNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/bevrijdingsmonumentEN.txt"));
                        }
                        break;
                    //chassetheater
                    case 15:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/chassetheaterNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/chassetheaterEN.txt"));
                        }
                        break;
                     //binding van isaac
                    case 16:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/bindingvanisaacNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/bindingvanisaacEN.txt"));
                        }
                        break;
                    //beyerd
                    case 17:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/beyerdNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/beyerdEN.txt"));
                        }
                        break;

                }
                Debug.WriteLine("Succesfully converted description to id.");
            }
            catch (Exception)
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
