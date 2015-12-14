using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Media;

namespace Breda_Ontdekt.Model.Entities
{
    public class Site : ObjectInfo
    {

        public Site(string name, Geoposition location, string description) : base(name, location)
        {
            base.description = description;
        }

  
    }
}
