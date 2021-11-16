using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using TestProject2.Helper;
using TestProject2.Model;

namespace APITestsCreateUser
{
    public class Tests
    {
        private readonly string _baseURL = "http://users.bugred.ru/tasks/rest";

        [Test]
        public void TestCreateUserValid()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/createuser");
            HelperData.ParamsUserData();
            CreateUser user = new CreateUser()
            {
                email = HelperData.GetEmailRandom(),
                name = HelperData.GetNameRandom(),
                tasks = new List<int>()
                {
                    34,
                    44
                },
                companies = new List<int>()
                {
                    56,
                    44
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
                tasks = new List<int>()
                {
                    34,
                    44
                },
                companies = new List<int>()
                {
                    56,
                    44
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

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("имя naruto уже есть в БД", json["message"]?.ToString());
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
                tasks = new List<int>()
                {
                    34,
                    44
                },
                companies = new List<int>()
                {
                    56,
                    44
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

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Пользователь с таким email уже существует ", json["message"]?.ToString());
        }
    }
}