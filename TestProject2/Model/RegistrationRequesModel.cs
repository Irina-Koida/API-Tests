using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TestProject2.Model
{
    class RegistrationRequesModel
    {
        public string email { get; set; }

        public string name { get; set; }

        public string password { get; set; }

        public RegistrationRequesModel(string email, string name, string password)
        {
            this.password = password;
            this.name = name;
            this.email = email;
        }

    }
}
