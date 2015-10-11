using Nancy;

namespace RestAPI
{
    public class RegisterModule : NancyModule
    {
        public RegisterModule()
        {
            Post["/"] = x =>
            {
                var userName = (string) Request.Form.UserName;
                var password = (string) Request.Form.Password;



                return new
                {
                    Token = ""
                };
            };


        }
    }

}