﻿using Breda_Ontdekt.Model.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage;
using Windows.Storage.Search;

namespace Breda_Ontdekt.Model
{
    public static class Storage
    {

        /// <summary>
        /// This methods saves the data in the local storage of the app
        /// </summary>
        /// <param name="saveData">a list with the data to save</param>
        /// <param name="filename">the filename (must end with .txt)</param>
        /// <returns></returns>
        public static async Task<bool> SaveMyListData(List<string> saveData, string filename)
        {
            try
            {
                StorageFile savedStuffFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                using (Stream writeStream = await savedStuffFile.OpenStreamForWriteAsync())
                {
                    DataContractSerializer stuffSerializer = new DataContractSerializer(typeof(List<string>));
                    stuffSerializer.WriteObject(writeStream, saveData);
                    await writeStream.FlushAsync();
                    writeStream.Dispose();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<List<string>> GetMyListData(string fileName)
        {
            var readStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(fileName);

            if (readStream == null)
                return new List<string>();

            DataContractSerializer stuffSerializer = new DataContractSerializer(typeof(List<string>));

            var setResult = (List<string>)stuffSerializer.ReadObject(readStream);
            return setResult;

        }

        public static double ConvertDegreeAngleToDouble(double degrees, double minutes, double seconds)
        {
            return degrees + (minutes / 60) + (seconds / 3600);
        }

        public static async Task<List<Site>> GetRouteInfo(string language)
        {
            List<Site> siteList = new List<Site>();
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/sites.csv"));
            String csv = await FileIO.ReadTextAsync(file);

            var alllines = csv.Split('\n');
            List<String> linesList = alllines.ToList();

            linesList.ForEach(l =>
            {
                var sepvals = l.Split(',');

                double latdegrees = Double.Parse(sepvals[1].Split('°')[0], CultureInfo.InvariantCulture);
                double latminutes = Double.Parse(sepvals[1].Split('°')[1], CultureInfo.InvariantCulture);

                double longdegrees = Double.Parse(sepvals[2].Split('°')[0], CultureInfo.InvariantCulture);
                double longminutes = Double.Parse(sepvals[2].Split('°')[1], CultureInfo.InvariantCulture);

                var geopos = new BasicGeoposition() { Latitude = ConvertDegreeAngleToDouble(latdegrees, latminutes, 0), Longitude = ConvertDegreeAngleToDouble(longdegrees, longminutes, 0) };
                Site s = new Site(sepvals[0], sepvals[3], new Geopoint(geopos), language);
                if ( Boolean.Parse(sepvals[4]))
                {
                    Debug.WriteLine("Is geofencingenabled site");
                    s.enableGeofencing();
                }
                else
                {
                    Debug.WriteLine("Is not geofencing enabled site");
                }
                siteList.Add(s);

            });
            siteList.ForEach(s => Debug.WriteLine(s.ToString()));

            List<Site> siteListImages = await AddImages(siteList);
            return siteListImages;
        }

        private static async Task<List<Site>> AddImages(List<Site> sites)
        {
            try
            {
                //get file from applicationfolder
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/SiteImagesNumbers.csv"));

                //get content from the file
                String content = await FileIO.ReadTextAsync(file);

                //split lines from content to a list
                List<String> lines = content.Split('\n').ToList();

                foreach(string line in lines)
                {
                    //seperate key and values from line
                    List<string> lineList = line.Split(':').ToList();

                    //get key
                    string key = lineList[0];

                    //get values
                    List<string> values = lineList[1].Split('&').ToList();
                    List<string> uriValues = new List<string>();
                    //parse string values to uri values

                    values.ForEach(v =>
                        uriValues.Add("/Assets/siteImages/" + v + ".jpg"));

                    //get video url
                    Uri url = new Uri("http://www.vvvbreda.nl");
                    try
                    {
                        if(lineList[2].Length > 2)
                            url = new Uri("https://" + lineList[2]);
                    }
                    catch { }

                    //search for same object
                    sites.ForEach(s =>
                    {
                        //if it is the same add the uris
                        if (s.name.Contains(key))
                        {
                            s.imageUrls = uriValues;
                            s.videoUrl = url;
                        }
                    });

                }
                return sites;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
            }
            return null;
        }
        
    }


}
