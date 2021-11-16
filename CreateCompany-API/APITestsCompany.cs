using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using TestProject2.Helper;
using TestProject2.Model;

namespace CreateCompany_API
{
    public class Tests
    {
        private readonly string _baseURL = "http://users.bugred.ru/tasks/rest";

        [Test]
        public void TestCreateCompanyValid()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/createcompany");
            HelperData.ParamsUserData();
            CreateCompany company = new CreateCompany()
            {
                company_name = "TestCompany",
                company_type = "���",
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
                company_type = "���",
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

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("������������ �� ������ c email_owner naruto152udzugmail.com", json["message"]?.ToString());
        }

        [Test]
        public void TestInvalidCreateCompanyWithEmptyEmailOwner()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/createcompany");
            HelperData.ParamsUserData();
            CreateCompany company = new CreateCompany()
            {
                company_name = "Susanno",
                company_type = "���",
                company_users = new List<string>()
                {
                    "amaterassy@gmail.com",
                    "akamelakill@gmail.com"
                },
                email_owner = ""
            };

            request.SendPostRequest(company);

            IRestResponse response = request.SendPostRequest(company);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("�������� email_owner �������� ������������!", json["message"]?.ToString());
        }

        [Test]
        public void TestInvalidCreateCompanyWithEmptyCompanyUsers()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/createcompany");
            HelperData.ParamsUserData();
            CreateCompany company = new CreateCompany()
            {
                company_name = "TestCompany",
                company_type = "���",
                company_users = new List<string>()
                {
                    "",
                    ""
                },
                email_owner = "naruto152udzu@gmail.com"
            };

            request.SendPostRequest(company);

            IRestResponse response = request.SendPostRequest(company);
            JObject json = JObject.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("company_users �� ������� ����������", json["message"]?.ToString());
        }

        [Test]
        public void TestCreateCompanyEmptyCompanyType()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/createcompany");
            HelperData.ParamsUserData();
            CreateCompany company = new CreateCompany()
            {
                company_name = "TestCompany",
                company_type = "",
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

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("company_type ������������", json["message"]?.ToString());
        }

        [Test]
        public void TestCreateCompanyWithEmptyCompanyName()
        {
            HelperRequest request = new HelperRequest(_baseURL + "/createcompany");
            HelperData.ParamsUserData();
            CreateCompany company = new CreateCompany()
            {
                company_name = "",
                company_type = "���",
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

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("company_name ������������", json["message"]?.ToString());
        }
    }
}