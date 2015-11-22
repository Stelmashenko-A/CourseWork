using System.Collections.Generic;
using System.Text;
using Nancy;
using Nancy.Json;
using Repository;
using TestData;

namespace SelfHostedRestAPI
{
    public class TweetModule : NancyModule
    {
        private readonly IStorage _storage;

        public TweetModule(IStorage storage )
        {
            _storage = storage;
            InitializeTweet();
        }

        protected void InitializeTweet()
        {
            Get["/tweets/user-time-line/{id:long}/{count:int}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(parameters.count, "user_time_line"));
            };

            Get["/tweets/user-time-line/{value}"] = parameters =>
            {
                var id = ulong.Parse(parameters.value);
                var r = _storage.GetUserLine(id, 0, 10, uint.MaxValue);
                JsonSettings.MaxJsonLength=int.MaxValue;
                var jsonBytes = Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(r));
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            };

            Get["/tweets/user-time-line-filtered/{id:long}/{count:int}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(parameters.count, "user-time-line-filtered"));
            };

            Get["/tweets/user-time-line-filtered/{id:long}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(5, "user-time-line-filtered"));
            };

            Get["/tweets/not-readed/{id:long}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(5, "not-readed"));
            };

            Get["/tweets/not-readed/{id:long}/{count:int}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(parameters.count, "not-readed"));
            };

            Get["/tweets/not-readed-filtered/{id:long}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(5, "not-readed-filtered"));
            };

            Get["/tweets/not-readed-filtered/{id:long}/{count:int}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(parameters.count, "not-readed-filtered"));
            };

            Get["/tweets/last-readed-id"] = _ =>
            {
                return 12345;
            };

            Get["/tweets/last-id"] = _ =>
            {
                return new JavaScriptSerializer().Serialize(123456);
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
