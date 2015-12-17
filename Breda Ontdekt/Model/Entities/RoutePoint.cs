using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Breda_Ontdekt.Model.Entities
{
    /// <summary>
    /// This enum contains all directions the user could make
    /// </summary>
    public enum direction { right, left, forward}

    public class RoutePoint : ObjectInfo
    {
        public direction direction { get; set; }

        public RoutePoint(string name, Geoposition position, direction direction): base(name, position)
        {
            this.direction = direction;
        }
    }
}
