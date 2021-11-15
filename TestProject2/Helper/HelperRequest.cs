using RestSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TestProject2.Helper
{
    class HelperRequest
    {
        private IRestClient _client;

        public int Timeout { get; set; }

        public HelperRequest(string requestUrl)
        {
            _client = new RestClient(requestUrl);
            Timeout = 30000;
        }

        public IRestResponse SendPostRequest(object body)
        {
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(body);
            IRestResponse response = _client.Execute(request);
            return response;
        }
    }
}

