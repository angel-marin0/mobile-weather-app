using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnweWeatherApp.Data;
using UnweWeatherApp.Model;

namespace UnweWeatherApp.Service
{
    public sealed class GoogleService
    {
        private static GoogleService instance = null;
        private static object locker = new object();
        private HttpClient client;

        public static GoogleService Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new GoogleService();
                    }
                    return instance;
                }
            }
        }

        private GoogleService()
        {
            client = new HttpClient();
        }

        public async Task<GoogleFindPlaceData> GetPhotoReference(string query)
        {
            GoogleFindPlaceData data = null;

            try
            {
                var response = await client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<GoogleFindPlaceData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }


            return data;
        }

        public async Task<byte[]> GetPhoto(string query)
        {
            byte[] output = null;

            try
            {
                var response = await client.GetByteArrayAsync(query);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            return output;
        }
    }
}
