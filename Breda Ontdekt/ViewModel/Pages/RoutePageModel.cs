using Breda_Ontdekt.Model;
using Breda_Ontdekt.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breda_Ontdekt.ViewModel
{
    public class RoutePageModel
    {
        private ObservableCollection<Route> _routes = new ObservableCollection<Route>();
        public ObservableCollection<Route> routes { get { return this._routes; } }

        public RoutePageModel()
        {
            LoadRoutes();
        }

        public async void LoadRoutes()
        {
            List<Site> sites = await Storage.GetRouteInfo();
            //todo load routes from class Storage
            //for testing: 
            Route route = new Route();
            foreach(Site s in sites)
            {
                route.addRoutePoint(s);
            }
            route.name = "historische kilometer";
            AddRoute(route);
        }

        public void AddRoute(Route route)
        {
            _routes.Add(route);
        }
    }

}
