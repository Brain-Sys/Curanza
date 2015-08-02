using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;

namespace BrainSys.UWP.Curanza.Network
{
    public class UniversalServiceClient
    {
        HttpClient client;

        public string ServiceUrl { get; private set; }
        public UniversalServiceClient(string serviceUrl)
        {
            this.ServiceUrl = serviceUrl;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("token", Guid.NewGuid().ToString());
            client.Timeout = TimeSpan.FromMinutes(10);
        }

        public async Task<TResponse> InvokeAsync<TRequest, TResponse>(TRequest request)
            where TResponse : BaseResponse where TRequest : BaseRequest
        {
            string requestTypeName = request.GetType().Name;
            string actionName = requestTypeName.Replace("Request", string.Empty).Replace("Dto", string.Empty);
            actionName = "Get" + actionName;
            var result = await this.InvokeAsync<TRequest, TResponse>(request, actionName);

            return result;
        }

        public async Task<TResponse> InvokeAsync<TRequest, TResponse>(TRequest request, string actionName)
            where TResponse : BaseResponse where TRequest : BaseRequest
        {
            string json = string.Empty;
            TResponse result = null;
            var response = await client.PostAsJsonAsync(this.ServiceUrl + actionName, request);

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<TResponse>(json);
            }

            return result;
        }

        public async Task<TResponse> InvokeAsync<TResponse>(string actionName)
        {
            string json = string.Empty;
            TResponse result;
            var response1 = await client.GetStringAsync(this.ServiceUrl + actionName);
            result = JsonConvert.DeserializeObject<TResponse>(response1);

            return result;
        }
    }
}