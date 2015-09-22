using System.Collections.Generic;
using Nancy.Json;

namespace RestAPI
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return View["index"];
            };

            Get["/tweets/user_time_line/{id:long}/{count:int}"] = parameters =>
            {
                var t = new Test("user_time_line");
                var l = new List<Test>();
                for (var i = 0; i < parameters.count; i++)
                {
                    l.Add(t);
                }
                return new JavaScriptSerializer().Serialize(l);
            };

            Get["/tweets/user_time_line/{id:long}"] = parameters =>
            {
                var t = new Test("user_time_line");
                var l = new List<Test> { t, t, t,t,t };

                return new JavaScriptSerializer().Serialize(l);
            };

            Get["/tweets/user_time_line_filtered/{id:long}/{count:int}"] = parameters =>
            {
                var t = new Test("user_time_line_filtered");
                var l = new List<Test>();
                for (var i = 0; i < parameters.count; i++)
                {
                    l.Add(t);
                }
                return new JavaScriptSerializer().Serialize(l);
            };

            Get["/tweets/user_time_line_filtered/{id:long}"] = parameters =>
            {
                var t = new Test("user_time_line_filtered");
                var l = new List<Test> { t, t, t, t, t };

                return new JavaScriptSerializer().Serialize(l);
            };


            Get["/tweets/not_readed/{id:long}"] = parameters =>
            {
                var t = new Test("not_readed");
                var l = new List<Test> { t, t, t, t, t };

                return new JavaScriptSerializer().Serialize(l);
            };

            Get["/tweets/not_readed/{id:long}/{count:int}"] = parameters =>
            {
                var t = new Test("not_readed");
                var l = new List<Test>();
                for (var i = 0; i < parameters.count; i++)
                {
                    l.Add(t);
                }
                return new JavaScriptSerializer().Serialize(l);
            };

            Get["/tweets/not_readed_filtered/{id:long}"] = parameters =>
            {
                var t = new Test("not_readed_filtered");
                var l = new List<Test> { t, t, t, t, t };

                return new JavaScriptSerializer().Serialize(l);
            };

            Get["/tweets/not_readed_filtered/{id:long}/{count:int}"] = parameters =>
            {
                var t = new Test("not_readed_filtered");
                var l = new List<Test>();
                for (var i = 0; i < parameters.count; i++)
                {
                    l.Add(t);
                }
                return new JavaScriptSerializer().Serialize(l);
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
    }

    public class Test
    {
        public Test(string field)
        {
            Field = field;
        }

        public string Field { get; private set; }
    }
}