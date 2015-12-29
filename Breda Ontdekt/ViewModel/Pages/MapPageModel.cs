using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breda_Ontdekt.Model.Entities;
using Windows.Devices.Geolocation;
using Breda_Ontdekt.Model;

namespace Breda_Ontdekt.ViewModel.Pages
{
    public class MapPageModel
    {
        public Route selectedRoute {get; set;}
        public Geolocator geolocator { get; set; }

        /// <summary>
        /// this method searchs for an object in selectedRoute with the same name 
        /// </summary>
        /// <param name="ObjectName"></param>
        public ObjectInfo GetObject(string objectName)
        {
            foreach(ObjectInfo o in selectedRoute.routePoints)
            {
                if (o.name.Equals(objectName))
                    return o;
            }

            return null;
        }

    }
}
