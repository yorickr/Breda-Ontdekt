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
                    //VVV
                    case 1:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/VVVNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/VVVEN.txt"));
                        }
                        break;
                    //liefdezuster
                    case 2:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/liefdezusterNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/liefdezusterEN.txt"));
                        }
                        break;
                    //nassaumonument
                    case 3:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/nassaumonumentNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/nassaumonumentEN.txt"));
                        }
                        break;
                    //Waypoint 1
                    case 4:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/WaypointNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/WaypointEN.txt"));
                        }
                        break;
                    //lighthouse
                    case 5:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/lighthouseNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/lighthouseEN.txt"));
                        }
                        break;
                    //Waypoint 2
                    case 6:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/Waypoint2NL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/Waypoint2EN.txt"));
                        }
                        break;
                    //Waypoint: einde park
                    case 7:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/ParkeindeNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/ParkeindeEN.txt"));
                        }
                        break;
                    //kasteel
                    case 8:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/kasteelNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/kasteelEN.txt"));
                        }
                        break;
                    //stadhouderspoort
                    case 9:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/stadhouderspoortNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/stadhouderspoortEN.txt"));
                        }
                        break;
                    //Waypoint: kruising Kasteelplein/Cingelstraat
                    case 10:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/Crossing1NL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/Crossing1EN.txt"));
                        }
                        break;
                    //Waypoint: bocht Cingelstraat
                    case 11:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/BochtCNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/BochtCEN.txt"));
                        }
                        break;
                    //huisvanbrecht
                    case 12:
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
                    case 13:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/spanjaardsgatNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/spanjaardsgatEN.txt"));
                        }
                        break;
                    //Begin vismarkt
                    case 14:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/14NL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/14EN.txt"));
                        }
                        break;
                    //Begin Havermarkt
                    case 15:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/15NL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/15EN.txt"));
                        }
                        break;
                    //torenstraat
                    case 16:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/torenstraatNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/torenstraatEN.txt"));
                        }
                        break;
                    //Grote Kerk
                    case 17:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/grotekerkNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/grotekerkEN.txt"));
                        }
                        break;
                    //Kruising Torenstraat/Kerkplein
                    case 18:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/18NL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/18EN.txt"));
                        }
                        break;
                    //Het poortje
                    case 19:
                        break;
                    //Ridderstraat
                    case 20:
                        break;
                    //Grote markt
                    case 21:
                        break;
                    //bevrijdingsmonument
                    case 22:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/bevrijdingsmonumentNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/bevrijdingsmonumentEN.txt"));
                        }
                        break;
                    //stadhuis
                    case 23:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/stadhuisNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/stadhuisEN.txt"));
                        }
                        break;
                    //Terug naar begin Grote Markt
                    case 24:
                        break;
                    //Zuidpunt Grote Markt
                    case 25:
                        break;                    
                    //antoniuskerk
                    case 26:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/antoniuskerkNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/antoniuskerkEN.txt"));
                        }
                        break;
                    //Kruising St.Janstraat/Molenstraat
                    case 27:
                        break;
                    //bibliotheek
                    case 28:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/bibliotheekNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/bibliotheekEN.txt"));
                        }
                        break;
                    //Kruising Molenstraat/Kloosterplein
                    case 29:
                        break;
                    //kloosterkazerne
                    case 30:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/kloosterkazerneNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/kloosterkazerneEN.txt"));
                        }
                        break;
                    //chassetheater
                    case 31:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/chassetheaterNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/chassetheaterEN.txt"));
                        }
                        break;
                    //Kruising Kloosterplein/Vlaszak
                    case 32:
                        break;
                    //binding van isaac
                    case 33:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/bindingvanisaacNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/bindingvanisaacEN.txt"));
                        }
                        break;
                    //Kruising Vlaszak/Boschstraat
                    case 34:
                        break;
                    //beyerd
                    case 35:
                        if (language == "NL")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/beyerdNL.txt"));
                        }
                        else if (language == "EN")
                        {
                            base.description = GetDescription(new Uri("ms-appx:///Assets/text/beyerdEN.txt"));
                        }
                        break;
                    //Kruising Vlaszak/Boschstraat
                    case 36:
                        break;
                    //Gasthuispoort
                    case 37:
                        break;
                    //Ingang Veemarktstraat
                    case 38:
                        break;
                    //1e bocht Veemarktstraat
                    case 39:
                        break;
                    //Kruising Veemarktstraat/St.Annastraat
                    case 40:
                        break;
                    //Ingang Willem Merkxtuin
                    case 41:
                        break;
                    //Kruising St.Annastraat/Catharinastraat
                    case 42:
                        break;
                    //Ingang Begijnenhof
                    case 43:
                        break;
                    //Kruising St.Annastraat/Catharinastraat
                    case 44:
                        break;
                    //Einde stadswandeling
                    case 45:
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
