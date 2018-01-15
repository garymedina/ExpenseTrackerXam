using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using ExpenseTrackerXam.Models;

namespace ExpenseTrackerXam.Helpers
{
    public static class ExpenseTrackerHttpClient
    {

        private static HttpClient currentClient = null;

        public static HttpClient GetClient()
        {
            //if (currentClient == null)
            //{

            //    currentClient = new HttpClient(new Marvin.HttpCache.HttpCacheHandler()
            //    {
            //        InnerHandler = new HttpClientHandler()
            //    });

            currentClient = new HttpClient();
                currentClient.BaseAddress = new Uri(Constants.ExpenseTrackerAPI);

                currentClient.DefaultRequestHeaders.Accept.Clear();
                currentClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            //}

            return currentClient;
        }
    }
}
