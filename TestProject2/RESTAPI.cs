using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using TestProject2.Helper;
using TestProject2.Model;

namespace TestProject2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

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

            Assert.AreEqual("OK", response.StatusCode.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(body.name, json["name"].ToString());
        }

        [Test]
        public void TestDoRegisterAUserValid()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/doregister");

            Dictionary<string, string> body = HelperData.GenerateUserData();
            request.SendPostRequest(body);

            IRestResponse response = request.SendPostRequest(body);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual("OK", response.StatusCode.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void TestDORegisterUserInvalidEmail()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/doregister");

            DORegister body = new DORegister()
            {
                email = "ddd",
                name = "Nameisname",
                password = "passP2345A"
            };

            request.SendPostRequest(body);

            IRestResponse response = request.SendPostRequest(body);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual("InternalServerError", response.StatusCode.ToString());
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public void TestAnAttemptToRegisterARegisteredUser()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/doregister");

            Dictionary<string, string> body = HelperData.ParamsUserData();
            request.SendPostRequest(body);

            IRestResponse response = request.SendPostRequest(body);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual("InternalServerError", response.StatusCode.ToString());
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public void TestCreateCompanyValid()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/createcompany");
            HelperData.ParamsUserData();
            CreateCompany company = new CreateCompany()
            {
                company_name = "TestCompany",
                company_type = "ООО",
                company_users = new List<string>()
                {
                    "test_anna@gmail.com",
                    "test_anna@gmail.com"
                },
                email_owner = "naruto152udzu@gmail.com"
            };

            request.SendPostRequest(company);

            IRestResponse response = request.SendPostRequest(company);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual("OK", response.StatusCode.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void TestCreateCompanyInvalidEmailOwner()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/createcompany");
            HelperData.ParamsUserData();
            CreateCompany company = new CreateCompany()
            {
                company_name = "Susanno",
                company_type = "ООО",
                company_users = new List<string>()
                {
                    "amaterassy@gmail.com",
                    "akamelakill@gmail.com"
                },
                email_owner = "naruto152udzugmail.com"
            };

            request.SendPostRequest(company);

            IRestResponse response = request.SendPostRequest(company);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual("InternalServerError", response.StatusCode.ToString());
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public void TestCreateUserValid()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/createuser");
            HelperData.ParamsUserData();
            CreateUser user = new CreateUser()
            {
                email = HelperData.GetEmailRandom(),
                name = HelperData.GetNameRandom(),
                tasks = new List<string>()
                {
                    "task one",
                    "task two"
                },
                companies = new List<string>()
                {
                    "test_anna@gmail.com",
                    "test_anna@gmail.com"
                },
                hobby = "reading",
                adres = "NY, USA, 2045",
                name1 = HelperData.GetNameRandom(),
                surname1 = HelperData.GetNameRandom(),
                fathername1 = HelperData.GetNameRandom(),
                cat = "dog",
                dog = "cat",
                parrot = "cavy",
                cavy = "parrot",
                hamster = "squirrel",
                squirrel = "hamster",
                phone = "444 444 4444",
                inn = "123456785012",
                gender = "m",
                birthday = "01.01.1800",
                date_start = "11.11.2020"
            };

            request.SendPostRequest(user);

            IRestResponse response = request.SendPostRequest(user);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual("OK", response.StatusCode.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void TestCreateUserInvalidExistUserName()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/createuser");
            HelperData.ParamsUserData();
            CreateUser user = new CreateUser()
            {
                email = HelperData.GetEmailRandom(),
                name = "naruto",
                tasks = new List<string>()
                {
                    "task one",
                    "task two"
                },
                companies = new List<string>()
                {
                    "test_anna@gmail.com",
                    "test_anna@gmail.com"
                },
                hobby = "reading",
                adres = "NY, USA, 2045",
                name1 = HelperData.GetNameRandom(),
                surname1 = HelperData.GetNameRandom(),
                fathername1 = HelperData.GetNameRandom(),
                cat = "dog",
                dog = "cat",
                parrot = "cavy",
                cavy = "parrot",
                hamster = "squirrel",
                squirrel = "hamster",
                phone = "444 444 4444",
                inn = "123456785012",
                gender = "m",
                birthday = "01.01.1800",
                date_start = "11.11.2020"
            };

            request.SendPostRequest(user);

            IRestResponse response = request.SendPostRequest(user);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual("InternalServerError", response.StatusCode.ToString());
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public void TestCreateUserInvalidExistUserEmail()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/createuser");
            HelperData.ParamsUserData();
            CreateUser user = new CreateUser()
            {
                email = "naruto152udzu@gmail.com",
                name = HelperData.GetNameRandom(),
                tasks = new List<string>()
                {
                    "task one",
                    "task two"
                },
                companies = new List<string>()
                {
                    "test_anna@gmail.com",
                    "test_anna@gmail.com"
                },
                hobby = "reading",
                adres = "NY, USA, 2045",
                name1 = HelperData.GetNameRandom(),
                surname1 = HelperData.GetNameRandom(),
                fathername1 = HelperData.GetNameRandom(),
                cat = "dog",
                dog = "cat",
                parrot = "cavy",
                cavy = "parrot",
                hamster = "squirrel",
                squirrel = "hamster",
                phone = "444 444 4444",
                inn = "123456785012",
                gender = "m",
                birthday = "01.01.1800",
                date_start = "11.11.2020"
            };

            request.SendPostRequest(user);

            IRestResponse response = request.SendPostRequest(user);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual("InternalServerError", response.StatusCode.ToString());
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

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

            Assert.AreEqual("OK", response.StatusCode.ToString());
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

            Assert.AreEqual("InternalServerError", response.StatusCode.ToString());
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

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