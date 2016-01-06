using System;
using System.Collections.ObjectModel;
using System.Threading;
using Windows.ApplicationModel.Background;
using Windows.Data.Json;
using Windows.Storage;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.UI.Notifications;
using System.Diagnostics;

namespace BackgroundTask
{
    public sealed class GeofenceBackgroundTask : IBackgroundTask
    {
        public GeofenceBackgroundTask()
        {
            Debug.WriteLine("Task running!");
        }

        void IBackgroundTask.Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            try
            {
                // Handle geofence state change reports
                GetGeofenceStateChangedReports(GeofenceMonitor.Current.LastKnownGeoposition);
            }
            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine("UnauthorizedAccess");
            }
            finally
            {
                deferral.Complete();
            }
        }

        private void GetGeofenceStateChangedReports(Geoposition pos)
        {
            GeofenceMonitor monitor = GeofenceMonitor.Current;

            Geoposition posLastKnown = monitor.LastKnownGeoposition;
            if ((posLastKnown.Coordinate.Point.Position.Latitude != pos.Coordinate.Point.Position.Latitude) || (posLastKnown.Coordinate.Point.Position.Longitude != pos.Coordinate.Point.Position.Longitude))
            {

            }
            string geofenceItemEvent = null;
            int numEventsOfInterest = 0;

            // Retrieve a vector of state change reports
            var reports = GeofenceMonitor.Current.ReadReports();

            foreach (var report in reports)
            {
                GeofenceState state = report.NewState;
                geofenceItemEvent = report.Geofence.Id;
                switch (state)
                {
                    case GeofenceState.Entered:
                        DoToast(numEventsOfInterest, geofenceItemEvent);
                        break;
                    case GeofenceState.Exited:
                        Debug.WriteLine("Exited geofence");
                        break;

                }


                
                   
            }
            
        }

        /// <summary>
        /// Helper method to pop a toast
        /// </summary>
        private void DoToast(int numEventsOfInterest, string eventName)
        {
            // pop a toast for each geofence event
            ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();

            // Create a two line toast and add audio reminder

            // Here the xml that will be passed to the 
            // ToastNotification for the toast is retrieved
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            // Set both lines of text
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode("Geolocation Sample"));

            if (1 == numEventsOfInterest)
            {
                toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(eventName));
            }
            else
            {
                string secondLine = "There are " + numEventsOfInterest + " new geofence events";
                toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(secondLine));
            }

            // now create a xml node for the audio source
            Windows.Data.Xml.Dom.IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotifier.Show(toast);
        }

       

       
    }
}
