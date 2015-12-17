using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
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
        private Geolocator geolocator;

        public MapPage()
        {
            this.InitializeComponent();

        }

        private async void DoRouting(object sender, RoutedEventArgs e)
        {
            MapRouteFinderResult routeFinderResult = await FindRoute();
            if (routeFinderResult.Status == MapRouteFinderStatus.Success)
            {
                MapRoute route = routeFinderResult.Route;

                // Draw all segments of route and add a Geofence for every turn:
                DrawRoute(route);
                await MapView.TrySetViewBoundsAsync(route.BoundingBox, null, MapAnimationKind.Linear);
            }
        }

        private async void doThings()
        {
            var route = await FindRoute();
            DrawRoute(route.Route);
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
            foreach (var leg in route.Legs)
            {
                DrawLeg(leg);
            }

            // Route has a Path containing all points to draw the route
            line.Path = new Geopath(route.Path.Positions);

            MapView.MapElements.Add(line);
        }

        private void DrawLeg(MapRouteLeg leg)
        {
            foreach (var maneuver in leg.Maneuvers)
            {
                DrawManeuver(maneuver);
            }
        }

        private void DrawManeuver(MapRouteManeuver maneuver)
        {
            //Retain this for later use
            //maneuverList.Add(new ManeuverDescription { Id = maneuverList.Count().ToString(), Location = maneuver.StartingPoint, Description = maneuver.InstructionText });

            var icon = new MapIcon
            {
                NormalizedAnchorPoint = new Point(0.5, 0.5),
                Location = maneuver.StartingPoint,
                ZIndex = 3
            };
            icon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/traffic.png"));
            MapView.MapElements.Add(icon);
        }


        private async Task<MapRouteFinderResult> FindRoute()
        {
            //const string beginLocation = "Lovensdijkstraat 63 Breda";
            //const string endLocation = "Lotusberg 35 Roosendaal";
            string beginLocation = fromField.Text;
            string endLocation = toField.Text;

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
                return routeResult;
            }

            return null;
        }

        private async void GeolocatorPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                await GeolocatorPositionChanged(args.Position);
            });
        }
        private async Task GeolocatorPositionChanged(Geoposition point)
        {
            // ... and another coordinate conversion
            var pos = new Geopoint(new BasicGeoposition { Latitude = point.Coordinate.Point.Position.Latitude, Longitude = point.Coordinate.Point.Position.Longitude });

            //DrawCarIcon(pos);
            DrawUserIcon(pos);

            //slower: DrawCarImage(pos);

            await MapView.TrySetViewAsync(pos, MapView.ZoomLevel, MapView.Heading, MapView.Pitch, MapAnimationKind.Linear);
        }

        const int carZIndewxz = 4;
        private void DrawCarIcon(Geopoint pos)
        {
            var carIcon = MapView.MapElements.OfType<MapIcon>().FirstOrDefault(p => p.ZIndex == carZIndewxz);
            if (carIcon == null)
            {
                carIcon = new MapIcon
                {
                    NormalizedAnchorPoint = new Point(0.5, 0.5),
                    ZIndex = carZIndewxz
                };
                carIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/car.png"));
                MapView.MapElements.Add(carIcon);
            }
            carIcon.Location = pos;
        }

        private void DrawUserIcon(Geopoint pos)
        {
            int userZIndex = 4;
            var userIcon = MapView.MapElements.OfType<MapIcon>().FirstOrDefault(p => p.ZIndex == userZIndex);
            if(userIcon == null)
            {
                userIcon = new MapIcon
                {
                    NormalizedAnchorPoint = new Point(0.5, 0.5),
                    ZIndex = userZIndex
                };
                userIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/user.png"));
                MapView.MapElements.Add(userIcon);
            }
            userIcon.Location = pos;
        }

        private async void GeofenceStateChanged(GeofenceMonitor sender, object args)
        {
            if (sender.Geofences.Any())
            {
                var reports = sender.ReadReports();
                foreach (var report in reports)
                {
                    switch (report.NewState)
                    {
                        case GeofenceState.Entered:
                            {
                                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                {
                                    //ManeuverDisplay.DisplayManeuver(maneuverList.Where(p => p.Id == report.Geofence.Id).First());
                                });
                                break;
                            }
                        case GeofenceState.Exited:
                            {
                                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                {
                                    //ManeuverDisplay.HideManeuver(report.Geofence.Id);
                                });
                                break;
                            }
                    }
                }
            }
        }

        public void ToggleTracking(object sender, RoutedEventArgs e)
        {
            if (geolocator == null)
            {
                geolocator = new Geolocator
                {
                    DesiredAccuracy = PositionAccuracy.High,
                    MovementThreshold = 1
                };
                geolocator.PositionChanged += GeolocatorPositionChanged;
                GeofenceMonitor.Current.GeofenceStateChanged += GeofenceStateChanged;
            }
            else
            {
                GeofenceMonitor.Current.GeofenceStateChanged -= GeofenceStateChanged;
                geolocator.PositionChanged -= GeolocatorPositionChanged;
                geolocator = null;
            }
        }




    }
}
