using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using TestProject2.Helper;
using TestProject2.Model;

namespace DoRegister_API
{
    public class Tests
    {
        private readonly string _baseURL = "http://users.bugred.ru/tasks/rest";

        [Test]
        public void TestValidRegistration()
        {
            RegistrationRequesModel body = new RegistrationRequesModel(
                HelperData.GetEmailRandom(), HelperData.GetNameRandom(), "Password12334");

            RestClient restClient = new RestClient(_baseURL + "/doregister");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(body);

            IRestResponse response = restClient.Execute(request);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(body.name, json["name"]?.ToString());
        }

        [Test]
        public void TestDoRegisterAUserValid()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/doregister");

            Dictionary<string, string> body = HelperData.GenerateUserData();
            request.SendPostRequest(body);

            IRestResponse response = request.SendPostRequest(body);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void TestDORegisterUserInvalidEmail()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/doregister");

            DORegister body = new DORegister()
            {
                email = "ddd",
                name = HelperData.GetNameRandom(),
                password = "passP2345A"
            };

            request.SendPostRequest(body);

            IRestResponse response = request.SendPostRequest(body);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Ќекоректный  email ddd", json["message"]?.ToString());
        }

        [Test]
        public void TestAnAttemptToRegisterARegisteredUser()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/doregister");

            Dictionary<string, string> body = HelperData.ParamsUserData();
            request.SendPostRequest(body);

            IRestResponse response = request.SendPostRequest(body);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("email naruto152udzu@gmail.com уже есть в базе", json["message"]?.ToString());
        }

        //сообщений нет, оно пропускает все, но € написала +- те сообщени€, которые €, гипотетически могла бы получить
        [TestCase("valid", "valid", "", "Empty password field")]
        [TestCase("valid", "valid", "1", "Minimum entry for the minimum allowed")]
        [TestCase("valid", "valid", "^!&^!&^!&^!&^!&^!&^!!!!!!&^!&^!&^!&", "Maximum entry for the maximum allowed")]
        public void TestInvalidRegistrationWithInvalidPassword(string email, string name, string password, string message)
        {
            HelperRequest request = new HelperRequest(_baseURL + "/doregister");
            if (email == "valid") email = HelperData.GetEmailRandom();
            if (name == "valid") name = HelperData.GetNameRandom();
            Dictionary<string, string> body = new Dictionary<string, string>
            {
                { "email", email },
                {"name", name },
                {"password", password }
            };
            IRestResponse response = request.SendPostRequest(body);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(message, json["message"]?.ToString());
        }

        [TestCase("", "valid", "valid", "ѕараметр email €вл€етс€ об€зательным!")]
        [TestCase("ddd", "valid", "valid", "Ќекоректный email ddd")]
        [TestCase("@gmail.com", "valid", "valid", "Ќекоректный email @gmail.com")]
        [TestCase("@gmail", "valid", "valid", "Ќекоректный email @gmail")]
        [TestCase("fhfhfjsiojff.com", "valid", "valid", "Ќекоректный email fhfhfjsiojff.com")]
        public void TestInvalidRegistrationWithInvalidEmail(string email, string name, string password, string message)
        {
            HelperRequest request = new HelperRequest(_baseURL + "/doregister");
            if (name == "valid") name = HelperData.GetNameRandom();
            if (password == "valid") password = "mySecretPass123";
            Dictionary<string, string> body = new Dictionary<string, string>
            {
                { "email", email },
                {"name", name },
                {"password", password }
            };
            IRestResponse response = request.SendPostRequest(body);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(message, json["message"]?.ToString());
        }

        [Test]
        public void TestInvalidRegistrationWithEmptyName()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/doregister");

            DORegister body = new DORegister()
            {
                email = HelperData.GetEmailRandom(),
                name = "",
                password = "passP2345A"
            };

            request.SendPostRequest(body);

            IRestResponse response = request.SendPostRequest(body);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("поле фио €вл€етс€ об€зательным", json["message"]?.ToString());
        }
    }
}