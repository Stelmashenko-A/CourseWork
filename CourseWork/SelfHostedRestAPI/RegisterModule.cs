using Nancy;
using Server;

namespace SelfHostedRestAPI
{
   /* public class RegisterModule : NancyModule
    {
        public RegisterModule()
        {
            Post["/register"] = x =>
            {
                var userName = (string)Request.Form.UserName;
                var password = (string)Request.Form.Password;
                var registrar = new Registrar();
                registrar.TryRegistrate(userName, password);
                return new
                {
                    Token = ""
                };
            };


        }
    }*/
}
