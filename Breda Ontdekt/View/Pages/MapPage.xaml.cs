using Breda_Ontdekt.Model;
using Breda_Ontdekt.Model.Entities;
using Breda_Ontdekt.ViewModel.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Popups;
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
        private MapPageModel model;
        private bool routeLoaded = false;
        private TransferClass transfer;

        public MapPage()
        {
            model = new MapPageModel();
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            //enable user tracking
            model.geolocator = new Geolocator
            {
                DesiredAccuracy = PositionAccuracy.High,
                MovementThreshold = 1
            };
            model.geolocator.PositionChanged += GeolocatorPositionChanged;
            GeofenceMonitor.Current.GeofenceStateChanged += GeofenceStateChanged;
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            transfer = (TransferClass)e.Parameter;
           
            if (transfer.isReturn == true)
            {
                returnHome();
            }
            if (transfer.resetted)
            {
                MapView.MapElements.Clear();
                model.selectedRoute = transfer.route;
                DrawRoute(model.selectedRoute);
                routeLoaded = true;
                transfer.resetted = false;
            }
            else
            {
                if (transfer.route != null && !routeLoaded)
                    try
                    {
                        //try to get route when navigate to this page
                        model.selectedRoute = transfer.route;

                        MapView.MapElements.Clear();
                        //draw all points of the route
                        DrawRoute(model.selectedRoute);
                        //DrawGeofences();
                        routeLoaded = true;
                    }
                    catch { }
            }
            
        }

        private async void returnHome()
        {
            if (model.geolocator != null)
            {
                model.geolocator = null;
            }
            Route r = new Route();
            var geopos = new BasicGeoposition() { Latitude = ConvertDegreeAngleToDouble(51, 35.6467, 0), Longitude = ConvertDegreeAngleToDouble(4, 46.7650, 0) };
            r.addRoutePoint(new ObjectInfo("VVV", new Geopoint(geopos), "1"));
            Geolocator geolocator = new Geolocator();
            Geoposition geoposition = null;
            geolocator.PositionChanged += GeolocatorPositionChanged;
            GeofenceMonitor.Current.GeofenceStateChanged += GeofenceStateChanged;
            geoposition = await geolocator.GetGeopositionAsync();
            Geopoint p = geoposition.Coordinate.Point;
            r.addRoutePoint(new ObjectInfo("", p, "0"));
            MapView.MapElements.Clear();
            DrawRoute(r);
        }

        public static double ConvertDegreeAngleToDouble(double degrees, double minutes, double seconds)
        {
            return degrees + (minutes / 60) + (seconds / 3600);
        }

        private async void DrawRoute(Route route)
        {
            //center map on the route and zoom the map
            ObjectInfo centerObject = model.GetObject("Begijnenhof");
            MapView.Center = centerObject.position;
            MapView.ZoomLevel = 15;

            //draw each object in from the route on the map
            foreach (ObjectInfo o in route.routePoints)
            {
                try
                {

                    MapIcon mapIcon1 = new MapIcon();
                    mapIcon1.Location = o.position;
                    mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    mapIcon1.Title = o.name;
                    mapIcon1.ZIndex = 0;
                    mapIcon1.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/routepoint.png"));
                    MapView.MapElements.Add(mapIcon1);
                    AddFence(o.id, o.position);

                    //if you want to delete the geofence locations, use this code:
                    //GeofenceMonitor.Current.Geofences.Clear();
                    //RemoveGeofences();

    }
                catch { }
            }


            //draw line between all the points
            ObjectInfo fromObject = null;
            foreach (ObjectInfo toObject in route.routePoints)
            {
                if (fromObject != null)
                {
                    MapRouteFinderResult routeResult = await MapRouteFinder.GetWalkingRouteAsync(fromObject.position, toObject.position);
                    if (routeResult.Status == MapRouteFinderStatus.Success)
                    {
                        MapRoute maproute = routeResult.Route;
                        // Draw all segments of route and add a Geofence for every turn:
                        DrawRoute(maproute);
                        // await MapView.TrySetViewBoundsAsync(maproute.BoundingBox, null, MapAnimationKind.Linear);
                    }
                }
                fromObject = toObject;
            }


        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HelpPage),transfer);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LanguagePage), transfer);
        }

        private void LanguageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LanguagePage), transfer);
        }

        //private async void DoRouting(object sender, RoutedEventArgs e)
        //{
        //    MapRouteFinderResult routeFinderResult = await FindRoute();
        //    if (routeFinderResult.Status == MapRouteFinderStatus.Success)
        //    {
        //        MapRoute route = routeFinderResult.Route;

        //        // Draw all segments of route and add a Geofence for every turn:
        //        DrawRoute(route);
        //        await MapView.TrySetViewBoundsAsync(route.BoundingBox, null, MapAnimationKind.Linear);
        //    }
        //}

        private void DrawRoute(MapRoute route)
        {
            //Draw a semi transparent fat green line
            var color = Colors.Green;
            color.A = 128;
            //MapView.MapElements.Clear();
            var line = new MapPolyline
            {
                StrokeThickness = 5,
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


        //private async Task<MapRouteFinderResult> FindRoute()
        //{
        //    //const string beginLocation = "Lovensdijkstraat 63 Breda";
        //    //const string endLocation = "Lotusberg 35 Roosendaal";
        //    string beginLocation = fromField.Text;
        //    string endLocation = toField.Text;

        //    // Get MapLocation for beginLocation and endLocation:
        //    MapLocationFinderResult result
        //        = await MapLocationFinder.FindLocationsAsync(beginLocation, MapView.Center);
        //    MapLocation from = result.Locations.First();

        //    result = await MapLocationFinder.FindLocationsAsync(endLocation, MapView.Center);
        //    MapLocation to = result.Locations.First();

        //    // Gets a driving route using the specified start and end coordinates.
        //    MapRouteFinderResult routeResult
        //    //    = await MapRouteFinder.GetDrivingRouteAsync(from.Point, to.Point);
        //          = await MapRouteFinder.GetWalkingRouteAsync(from.Point, to.Point);

        //    //stuff die in een heeeele andere methode gedaan moet worden
        //    MapView.Center = from.Point;
        //    MapView.ZoomLevel = 15;

        //    if (routeResult.Status == MapRouteFinderStatus.Success)
        //    {
        //        return routeResult;
        //    }

        //    return null;
        //}

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
            if (userIcon == null)
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
                                    Debug.WriteLine("in geofence");
                                    //new MessageDialog("in geofence").ShowAsync().AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                                });
                                break;
                            }
                        case GeofenceState.Exited:
                            {
                                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                {
                                    //ManeuverDisplay.HideManeuver(report.Geofence.Id);
                                    Debug.WriteLine("uit geofence");
                                    //new MessageDialog("uit geofence").ShowAsync().AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                                });
                                break;
                            }
                    }
                }
            }
        }

        public void ToggleTracking(object sender, RoutedEventArgs e)
        {
            if (model.geolocator == null)
            {
                model.geolocator = new Geolocator
                {
                    DesiredAccuracy = PositionAccuracy.High,
                    MovementThreshold = 1
                };
                model.geolocator.PositionChanged += GeolocatorPositionChanged;
                GeofenceMonitor.Current.GeofenceStateChanged += GeofenceStateChanged;
            }
            else
            {
                GeofenceMonitor.Current.GeofenceStateChanged -= GeofenceStateChanged;
                model.geolocator.PositionChanged -= GeolocatorPositionChanged;
                model.geolocator = null;
            }
        }

        private async void test(object sender, RoutedEventArgs e)
        {

            (await Storage.GetRouteInfo("EN")).ForEach(s =>
            {
                MapIcon mapIcon1 = new MapIcon();
                mapIcon1.Location = s.position;
                mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
                mapIcon1.Title = "Space Needle";
                mapIcon1.ZIndex = 0;

                MapView.MapElements.Add(mapIcon1);
            });
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.instance.SwitchMenu();
        }

        //when the user clicks on the map this method is called
        private void MapView_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            //get mapIcon from args
            MapIcon clickedIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            ObjectInfo o = model.GetObject(clickedIcon.Title);
            transfer.info = o;
            //navigate to info page
            Frame.Navigate(typeof(InfoPage), transfer);
        }

        const int fenceIndex = 1;
        private void DrawGeofences()
        {
            //Draw semi transparent purple circles for every fence
            var color = Colors.Purple;
            color.A = 80;

            // Note GetFenceGeometries is a custom extension method
            //foreach (var pointlist in GeofenceMonitor.Current.GetFenceGeometries())
            //{
            //    var shape = new MapPolygon
            //    {
            //        FillColor = color,
            //        StrokeColor = color,
            //        Path = new Geopath(pointlist.Select(p => p.Position)),
            //        ZIndex = fenceIndex

            //    };
            //    MapView.MapElements.Add(shape);
            //}
        }

        private void RemoveGeofences()
        {
            var routeFences = MapView.MapElements.Where(p => p.ZIndex == fenceIndex).ToList();
            foreach (var fence in routeFences)
            {
                MapView.MapElements.Remove(fence);
            }
        }

        /// <summary>
        /// Creation 
        /// </summary>
        public void AddFence(string key, Geopoint position)
        {
            // Replace if it already exists for this maneuver key
            var oldFence = GeofenceMonitor.Current.Geofences.Where(p => p.Id == key).FirstOrDefault();
            if (oldFence != null)
            {
                GeofenceMonitor.Current.Geofences.Remove(oldFence);
            }

            Geocircle geocircle = new Geocircle(position.Position, 25);

            bool singleUse = false;

            MonitoredGeofenceStates mask = 0;

            mask |= MonitoredGeofenceStates.Entered;
            mask |= MonitoredGeofenceStates.Exited;

            var geofence = new Geofence(key, geocircle, mask, singleUse, TimeSpan.FromSeconds(1));
            GeofenceMonitor.Current.Geofences.Add(geofence);
        }

    }
}
