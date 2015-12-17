using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

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
    }

    
}
