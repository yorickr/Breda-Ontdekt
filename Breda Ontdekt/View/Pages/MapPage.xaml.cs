﻿using Breda_Ontdekt.Model;
using Breda_Ontdekt.Model.Entities;
using Breda_Ontdekt.ViewModel.Pages;
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
        private MapPageModel model;
        private bool routeLoaded = false;
        private TransferClass transfer;

        public MapPage()
        {
            model = new MapPageModel();
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            transfer = (TransferClass)e.Parameter;
           
            if (transfer.isReturn == true)
            {
                returnHome();
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
            r.addRoutePoint(new ObjectInfo("VVV", new Geopoint(geopos)));
            Geolocator geolocator = new Geolocator();
            Geoposition geoposition = null;
            geolocator.PositionChanged += GeolocatorPositionChanged;
            GeofenceMonitor.Current.GeofenceStateChanged += GeofenceStateChanged;
            geoposition = await geolocator.GetGeopositionAsync();
            Geopoint p = geoposition.Coordinate.Point;
            r.addRoutePoint(new ObjectInfo("", p));
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
                    DrawObjectInfoIcon(o);
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

        public void DrawObjectInfoIcon(ObjectInfo objectInfo)
        {
            MapIcon mapIcon1 = new MapIcon();
            mapIcon1.Location = objectInfo.position;
            mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon1.Title = objectInfo.name;
            mapIcon1.ZIndex = 0;
            if (!objectInfo.isPassed)
                mapIcon1.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/routepoint.png"));
            else
                mapIcon1.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/routepoint_seen.png"));

            MapView.MapElements.Add(mapIcon1);
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

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.instance.SwitchMenu();
        }

        public bool ChangeObjectChanged(int objectID)
        {

            //check for each object in the route if the index is equal to objectID
            foreach(ObjectInfo o in model.selectedRoute.routePoints)
            {
                //change -1 to o.index when is updated
                if( - 1 /*o.index*/ == objectID)
                    DrawObjectInfoIcon(o);
                    return true;
            }
            return false;
        }

        //when the user clicks on the map this method is called
        private void MapView_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            //get mapIcon from args
            MapIcon clickedIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            ObjectInfo o = model.GetObject(clickedIcon.Title);

            //for testing:
            o.isPassed = true;
            DrawObjectInfoIcon(o);

            transfer.info = o;
            //navigate to info page
            Frame.Navigate(typeof(InfoPage), transfer);
        }
    }
}
