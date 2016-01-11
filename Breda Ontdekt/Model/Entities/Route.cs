using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breda_Ontdekt.Model.Entities
{
    public class Route
    {
        public List<ObjectInfo> routePoints { get; set; }
        public string name { get; set; }

        public Route()
        {
            routePoints = new List<ObjectInfo>();
        }

        public void addRoutePoint(ObjectInfo objectInfo)
        {
            routePoints.Add(objectInfo);
        }
    }
}
