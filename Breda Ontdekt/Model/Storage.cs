using Breda_Ontdekt.Model.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    DataContractSerializer stuffSerializer = new DataContractSerializer(typeof(List<string> ) );
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

            if(readStream == null)
                return new List<string>();

            DataContractSerializer stuffSerializer = new DataContractSerializer(typeof(List<string>));

            var setResult = (List<string>)stuffSerializer.ReadObject(readStream);
            return setResult;

        }

        public static double ConvertDegreeAngleToDouble(double degrees, double minutes, double seconds)
        {
            return degrees + (minutes / 60) + (seconds / 3600);
        }

        public static async Task<List<Site>> GetRouteInfo()
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

                double latdegrees = Double.Parse(sepvals[1].Split('°')[0]);
                double latminutes = Double.Parse(sepvals[1].Split('°')[1]);

                double longdegrees = Double.Parse(sepvals[2].Split('°')[0]);
                double longminutes = Double.Parse(sepvals[2].Split('°')[1]);
                
                var geopos = new BasicGeoposition() { Latitude = ConvertDegreeAngleToDouble(latdegrees,latminutes,0), Longitude = ConvertDegreeAngleToDouble(longdegrees,longminutes,0)};
                siteList.Add(new Site(sepvals[3],new Geopoint(geopos), sepvals[4]));
                
            });
            siteList.ForEach(s => Debug.WriteLine(s.ToString()));

            return siteList;
        }
    }

    
}
