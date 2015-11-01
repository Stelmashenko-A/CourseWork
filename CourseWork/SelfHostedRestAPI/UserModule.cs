using System.Collections.Generic;
using Nancy;
using Nancy.Json;
using Repository;
using TestData;

namespace SelfHostedRestAPI
{
    public class UserModule : NancyModule
    {

        private readonly IRepository<Test> _repository = new TestRepository();

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

            Get["/user/{id:long}/send_tweet/{value:alpha}"] = parameters =>
            {
                var t = new Test(parameters.value);
                _repository.Add((1), t);
                return new JavaScriptSerializer().Serialize(t);
            };
        }
    }
}
