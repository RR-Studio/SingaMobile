namespace SingaMobile.Models
{
    class RegistrationModel
    {
        public string email { get; set; }
        public string password { get; set; }

        public RegistrationModel(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }

    class RegistrationErrorModel
    {
        public string code { get; set; }
        public string description { get; set; }
    }
}