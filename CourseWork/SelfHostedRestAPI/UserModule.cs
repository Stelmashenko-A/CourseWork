using System.Collections.Generic;
using Nancy;
using Nancy.Json;
using Repository;
using TestData;

namespace SelfHostedRestAPI
{
    public class UserModule : NancyModule
    {

        public UserModule()
        {
            InitializeUser();
        }

        protected void InitializeUser()
        {
            Get["/user/{id:long}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(new Test("user"));
            };

            Get["/user/{id:long}/filter/add/{filterName:alpha}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(new Test(parameters.filterName));
            };

            Get["/user/{id:long}/filter/remove/{filterName:alpha}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(new Test(parameters.filterName));
            };

            Get["/user/{id:long}/filter/all"] = parameters =>
            {
                var l = new List<Test> { new Test("F1"), new Test("F2"), new Test("f3") };
                return new JavaScriptSerializer().Serialize(l);
            };
        }
    }
}
