using System;
using System.Collections.Generic;

namespace TestProject2.Helper
{
   static public class HelperData
    {
        static public Dictionary<string, string> ParamsUserData()
        {
            return new Dictionary<string, string>()
            {
                {"email", "naruto152udzu@gmail.com"},
                {"name", "naruto"},
                {"password", "naruto12345"},
            };
        }

        static public Dictionary<string, string> GenerateUserData()
        {
            string now = DateTime.Now.ToString("ddMMyyyyhhmmss");
            string email = now + "@test.com";
            string name = "name" + now;

            return new Dictionary<string, string>()
            {
                {"email", email },
                {"name", name },
                {"password", "mySecretPass123" },

            };
        }

        static public string GetNameRandom()
        {
            return "Harry" + DateTime.Now.ToString("hhmmssMMddyy");
        }

        static public string GetEmailRandom()
        {
            return DateTime.Now.ToString("hhmmssMMddyy") + "@gmail.com";
        }

    }
}
