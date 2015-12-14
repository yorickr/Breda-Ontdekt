using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Breda_Ontdekt.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        public MapPage()
        {
            this.InitializeComponent();
            doThings();


        }

        private async void doThings()
        {
            var route = await FindRoute();
            DrawRoute(route);
        }

        private void DrawRoute(MapRoute route)
        {
            //Draw a semi transparent fat green line
            var color = Colors.Green;
            color.A = 128;
            MapView.MapElements.Clear();
            var line = new MapPolyline
            {
                StrokeThickness = 11,
                StrokeColor = color,
                StrokeDashed = false,
                ZIndex = 2
            };

            // Route has legs, legs have maneuvers
            //foreach (var leg in route.Legs)
            //{
            //    DrawLeg(leg);
            //}

            // Route has a Path containing all points to draw the route
            line.Path = new Geopath(route.Path.Positions);

            MapView.MapElements.Add(line);
        }


        private async Task<MapRoute> FindRoute()
        {
            const string beginLocation = "Lovensdijkstraat 63 Breda";
            const string endLocation = "Lotusberg 35 Roosendaal";

            // Get MapLocation for beginLocation and endLocation:
            MapLocationFinderResult result
                = await MapLocationFinder.FindLocationsAsync(beginLocation, MapView.Center);
            MapLocation from = result.Locations.First();

            result = await MapLocationFinder.FindLocationsAsync(endLocation, MapView.Center);
            MapLocation to = result.Locations.First();

            // Gets a driving route using the specified start and end coordinates.
            MapRouteFinderResult routeResult
            //    = await MapRouteFinder.GetDrivingRouteAsync(from.Point, to.Point);
                  = await MapRouteFinder.GetWalkingRouteAsync(from.Point, to.Point);

            //stuff die in een heeeele andere methode gedaan moet worden
            MapView.Center = from.Point;
            MapView.ZoomLevel = 15;

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                return routeResult.Route;
            }

            return null;
        }




    }
}
