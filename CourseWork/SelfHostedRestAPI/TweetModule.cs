using System.Collections.Generic;
using Nancy;
using Nancy.Json;
using Repository;
using TestData;

namespace SelfHostedRestAPI
{
    public class TweetModule : NancyModule
    {
        private readonly TestRepository _repository = new TestRepository();

        public TweetModule()
        {
            InitializeTweet();
        }

        protected void InitializeTweet()
        {
            Get["/tweets/user_time_line/{id:long}/{count:int}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(parameters.count, "user_time_line"));
            };

            Get["/tweets/user_time_line/{id:long}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(_repository.GetAll(parameters.id));
            };

            Get["/tweets/user_time_line_filtered/{id:long}/{count:int}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(parameters.count, "user_time_line_filtered"));
            };

            Get["/tweets/user_time_line_filtered/{id:long}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(5, "user_time_line_filtered"));
            };

            Get["/tweets/not_readed/{id:long}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(5, "not_readed"));
            };

            Get["/tweets/not_readed/{id:long}/{count:int}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(parameters.count, "not_readed"));
            };

            Get["/tweets/not_readed_filtered/{id:long}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(5, "not_readed_filtered"));
            };

            Get["/tweets/not_readed_filtered/{id:long}/{count:int}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(parameters.count, "not_readed_filtered"));
            };

            Get["/tweets/last_readed_id"] = _ =>
            {
                return 12345;
            };

            Get["/tweets/last_id"] = _ =>
            {
                return 123456;
            };
        }

        private static List<Test> ListTest(int num, string s)
        {
            var t = new Test(s);
            var l = new List<Test>();
            for (var i = 0; i < num; i++)
            {
                l.Add(t);
            }
            return l;
        }
    }
}
