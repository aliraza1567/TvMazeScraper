using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TvMaze.Domain.Models;
using TvMaze.Infrastructure.Abstractions.TvMaze.Clients;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TvMaze.External.Clients.Contracts.Show;
using TvMaze.External.Exceptions;

namespace TvMaze.External.Clients
{
    public class TvMazeClient: ITvMazeClient
    {
        public const string ClientName = "TvMazeClient";
        private readonly ILogger<TvMazeClient> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _uri;
        private readonly IMapper _mapper;

        public TvMazeClient(ILogger<TvMazeClient> logger, IHttpClientFactory httpClientFactory, string uri, IMapper mapper)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _uri = uri;
            _mapper = mapper;
        }

        public async Task<List<Show>> GetAllShowsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var ticketQueuesDto = await SendRequestAsync<ShowDto>(HttpMethod.Get, "shows", null, cancellationToken);
                var ticketQueues = _mapper.Map<List<Show>>(ticketQueuesDto);
                return ticketQueues;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task SendRequestAsync(HttpMethod method, string action, object data, CancellationToken cancellationToken)
        {
            await SendRequestAsync<HttpResponseMessage>(method, action, data, cancellationToken);
        }

        private async Task<T> SendRequestAsync<T>(HttpMethod method, string action, object data, CancellationToken cancellationToken, bool allowNull = false)
        {
            using var request = CreateRequest(method, action, data);

            HttpResponseMessage response;
            try
            {
                var client = _httpClientFactory.CreateClient(ClientName);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.SendAsync(request, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while executing request.");
                throw new TvMazeApiException(e);
            }

            if (!response.IsSuccessStatusCode)
            {
                string content = "";
                try
                {
                    content = await ReadAsStringAsync(response.Content);
                }
                catch (Exception)
                {
                    // ignored
                }

                _logger.LogError("The Api responded with the error code: {statusCode}. {content}", response.StatusCode, content);
                throw new TvMazeApiException($"The Api responded with the error code: {response.StatusCode}. {content}", response);
            }

            _logger.LogInformation("The Api responded with the status code: {statusCode}", response.StatusCode);

            // Allow empty/manual results by specifying HttpResponseMessage
            if (response is T result)
            {
                return result;
            }

            try
            {
                var content = await ReadAsStringAsync(response.Content);

                _logger.LogInformation("{content}", content);

                result = JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception e)
            {
                throw new TvMazeApiException("Something went wrong while reading the result.", e);
            }

            if (!allowNull && result == null)
            {
                throw new Exception("The response from the Api resulted in an error.");
            }

            return result;
        }

        private async Task<string> ReadAsStringAsync(HttpContent content)
        {
            var bytes = await content.ReadAsByteArrayAsync();
            return Encoding.UTF8.GetString(bytes);
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string action, object data)
        {
            var request = new HttpRequestMessage(method, new Uri(new Uri(_uri), $"{action}"));

            var json = "";
            if (data != null)
            {
                json = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            else
            {
                request.Content = new StringContent("", Encoding.UTF8, "application/json");
            }

            _logger.LogInformation("Executing api request: {method} {url} {body}", method.ToString(), action, json);

            return request;
        }
    }
}
