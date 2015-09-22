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

            InitializeTweet();

            InitializeFilter();

        }

        protected void InitializeTweet()
        {

            Get["/tweets/user_time_line/{id:long}/{count:int}"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize(ListTest(parameters.count, "user_time_line"));
            };

            Get["/tweets/user_time_line/{id:long}"] = parameters =>
            {

                return new JavaScriptSerializer().Serialize(ListTest(5, "user_time_line"));
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

        protected void InitializeFilter()
        {
            Get["/filter"] = parameters =>
            {
                var l = new List<Test> { new Test("F1"), new Test("F2"), new Test("f3") };
                return new JavaScriptSerializer().Serialize(l);
            };
        }


        private List<Test> ListTest(int num, string s)
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

    public class Test
    {
        public Test(string field)
        {
            Field = field;
        }

        public string Field { get; private set; }
    }

}