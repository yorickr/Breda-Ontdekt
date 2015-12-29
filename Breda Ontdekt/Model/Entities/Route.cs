using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breda_Ontdekt.Model.Entities
{
    public class Route
    {
        public List<RoutePoint> routePoints { get; set; }

        public Route()
        {
            routePoints = new List<RoutePoint>();
        }

        public void addRoutePoint(RoutePoint routePoint)
        {
            routePoints.Add(routePoint);
        }
    }
}
