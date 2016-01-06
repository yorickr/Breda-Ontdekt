using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Breda_Ontdekt.Model
{
    public  class ObjectInfo
    {
        //the position of the object
        public Geopoint position { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isPassed { get; set; }
        public string id { get; set; }
        public List<string> imageUrls { get; set; }

        public ObjectInfo(string name, Geopoint position, string id)
        {
            this.name = name;
            this.position = position;
            isPassed = false;
            this.id = id;
            this.imageUrls = new List<string>();

        }

    }
}
