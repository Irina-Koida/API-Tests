using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using TestProject2.Helper;
using TestProject2.Model;

namespace TestProject2
{
    public class Tests
    {
        private readonly string _baseURL = "http://users.bugred.ru/tasks/rest";

        [Test]
        public void TestMagicSearchByEmailValid()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/magicsearch");

            Dictionary<string, string> body = new Dictionary<string, string>
            {
                {"query","naruto152udzu@gmail.com"}
            };

            request.SendPostRequest(body);

            IRestResponse response = request.SendPostRequest(body);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual("231", response.StatusCode.ToString());
            Assert.AreEqual("naruto152udzu@gmail.com", json["results"][0]["email"]?.ToString());
        }

        [Test]
        public void TestEmptyFieldMagicSearch()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/magicsearch");

            Dictionary<string, string> body = new Dictionary<string, string>
            {
                {"query",""}
            };
            request.SendPostRequest(body);

            IRestResponse response = request.SendPostRequest(body);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual("455", response.StatusCode.ToString());
            Assert.AreEqual("Не найден обязательный параметр query", json["message"]?.ToString());
        }
    }
}