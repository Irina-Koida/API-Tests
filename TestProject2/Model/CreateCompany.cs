using System.Collections.Generic;

namespace TestProject2.Model
{
    public class CreateCompany
    {
        public string company_name { get; set; }

        public string company_type { get; set; }

        public List<string> company_users { get; set; }

        public string email_owner { get; set; }
    }
}
