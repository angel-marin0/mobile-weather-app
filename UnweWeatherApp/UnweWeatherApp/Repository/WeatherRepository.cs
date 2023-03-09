using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnweWeatherApp.Model;

namespace UnweWeatherApp.Repository
{
    public class WeatherRepository
    {
        readonly SQLiteAsyncConnection database;

        public WeatherRepository(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<WeatherModel>().Wait();
        }

        /// <summary>
        /// Saves ("caches") weather information for newly searched locations.
        /// </summary>
        /// <param name="weatherModel"> The entity to be persisted </param>
        /// <returns> Number of rows affected </returns>
        public async Task<int> Save(WeatherModel weatherModel)
        {
            return await database.InsertAsync(weatherModel);
        }


        /// <summary>
        /// An entry is considered cached (pseudo-cached in this case) if it's more recent than 15 minutes.
        /// </summary>
        /// <param name="locationTitle"> The city that is looked up </param>
        /// <returns> Weather information for a recently searched location </returns>
        public async Task<WeatherModel> GetByLocationTitle(string locationTitle)
        {
            TimeSpan timeSpan = TimeSpan.FromMinutes(15);
            DateTime target = DateTime.Now.Subtract(timeSpan);

            return await database.Table<WeatherModel>()
                            .Where(wm => wm.Title.ToLower().Equals(locationTitle.ToLower()) &&
                             target <= wm.Time)
                            .OrderByDescending(wm => wm.Time)
                            .FirstOrDefaultAsync();
        }
    }
}
