using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Breda_Ontdekt.Model
{
    public abstract class ObjectInfo
    {
        //the position of the object
        public Geopoint position { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public ObjectInfo(string name, Geopoint position)
        {
            this.name = name;
            this.position = position;
        }

    }
}
