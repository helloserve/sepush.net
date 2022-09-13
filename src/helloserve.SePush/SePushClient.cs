using helloserve.SePush.Models;
using helloserve.SePush.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace helloserve.SePush
{
    public class SePushClient : ISePush
    {
        static HttpClient httpClient;

        readonly ILogger<SePushClient> logger;
        readonly IOptionsMonitor<SePushOptions> options;

        public SePushClient(IOptionsMonitor<SePushOptions> options, ILogger<SePushClient> logger, HttpClient httpClient = null)
        {
            this.options = options;
            this.logger = logger;
            SePushClient.httpClient = httpClient ?? new HttpClient();
        }

        private string GetQueryParam(Tuple<string, string> param)
        {
            if (string.IsNullOrEmpty(param.Item2))
                return $"{param.Item1}";

            return FormattableString.Invariant($"{param.Item1}={HttpUtility.UrlEncode(param.Item2)}");
        }

        private string GetApiUrl(string route, params Tuple<string, string>[] query)
        {
            var queryParams = string.Empty;
            if (query.Length > 0)
            {
                queryParams = FormattableString.Invariant($"?{string.Join("&",query.Select(x => GetQueryParam(x)))}");
            }

            string url = FormattableString.Invariant($"{options.CurrentValue.ApiUrl}{options.CurrentValue.Version}/{route}{queryParams}");


            return url;
        }

        private HttpRequestMessage BuildGetRequestMessage(string route, params Tuple<string, string>[] query)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, GetApiUrl(route, query));
            request.Headers.Add("Token", options.CurrentValue.Token);

            return request;
        }

        private async Task<TResult> GetResponse<TResponse, TResult>(HttpRequestMessage request, Func<TResponse, TResult> resultFunc)
        {
            var response = await httpClient.SendAsync(request).ConfigureAwait(false);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Error occured sending request to {request.RequestUri}: HTTP {response.StatusCode}");
            }

            var result = JsonSerializer.Deserialize<TResponse>(await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false));
            return resultFunc(result);
        }

        public async Task<Status> StatusAsync()
        {
            var request = BuildGetRequestMessage("status");
            return await GetResponse<StatusResponse, Status>(request, r => r).ConfigureAwait(false);
        }

        public async Task<AreaInformation> AreaInformationAsync(string id, string testMode = null)
        {
            bool isTestMode = !string.IsNullOrEmpty(testMode);

            Tuple<string, string>[] queryParams = new Tuple<string, string>[1 + (isTestMode ? 1 : 0)];
            
            if (isTestMode)
            {
                queryParams[0] = new Tuple<string, string>("test", testMode);
            }
            queryParams[1] = id.AsQueryParam("id");

            var request = BuildGetRequestMessage("area", queryParams);
            return await GetResponse<AreaInformationResponse, AreaInformation>(request, r => r).ConfigureAwait(false);
        }

        public async Task<IEnumerable<AreaNearby>> AreasNearbyAsync(double latitude, double longitude)
        {
            var request = BuildGetRequestMessage("areas_nearby", latitude.AsQueryParam("lat"), longitude.AsQueryParam("lon"));
            return await GetResponse<AreaNearbyResponse, IEnumerable<AreaNearby>>(request, r => r.Areas).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Area>> AreasSearchAsync(string text)
        {
            var request = BuildGetRequestMessage("areas_search", text.AsQueryParam("text"));
            return await GetResponse<AreaSearchResponse, IEnumerable<Area>>(request, r => r.Areas).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Topic>> TopicsNearbyAsync(double latitude, double longitude)
        {
            var request = BuildGetRequestMessage("topics_nearby", latitude.AsQueryParam("lat"), longitude.AsQueryParam("lon"));
            return await GetResponse<NearbyTopicsResponse, IEnumerable<Topic>>(request, r => r.Topics).ConfigureAwait(false);
        }

        public async Task<Allowance> CheckAllowanceAsync()
        {
            var request = BuildGetRequestMessage("api_allowance");
            return await GetResponse<CheckAllowanceResponse, Allowance>(request, r => r.Allowance).ConfigureAwait(false);
        }
    }
}
