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
        public Geoposition position { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public ObjectInfo(string name, Geoposition position)
        {
            this.name = name;
            this.position = position;
        }

    }
}
