using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using TestProject2.Helper;

namespace APITestsDeleteAvatar
{
    public class Tests
    {
        private readonly string _baseURL = "http://users.bugred.ru/tasks/rest";

        [Test]
        public void TestDeleteAvatarValid()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/deleteavatar");
            Dictionary<string, string> body = new Dictionary<string, string>
            {
                {"email","naruto152udzu@gmail.com"}
            };
            request.SendPostRequest(body);

            IRestResponse response = request.SendPostRequest(body);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void TestDeleteAvatarInvalid()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/deleteavatar");
            Dictionary<string, string> body = new Dictionary<string, string>
            {
                {"email",""}
            };
            request.SendPostRequest(body);

            IRestResponse response = request.SendPostRequest(body);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}