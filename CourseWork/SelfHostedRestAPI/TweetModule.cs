using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Json;
using Nancy.Security;
using Repository;
using Server;
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

            Post["/tweets/user-time-line/lineHead/{value}"] = parameters =>
            {
                var id = ulong.Parse(parameters.value);
                return _storage.GetLineHead(id);
            };

            Post["/tweets/replies/{userId}/{tweetId}"] = parameters =>
            {
                var userId = long.Parse(parameters.userId);
                var tweetId = ulong.Parse(parameters.tweetId);
                var claims = (string)userId.ToString();
                this.RequiresClaims(new[] { claims });
                var replyLoader = new ReplyLoader();

                var r = replyLoader.LoadReplies(userId, tweetId);
                JsonSettings.MaxJsonLength = int.MaxValue;
                var jsonBytes = Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(r));
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            };

            Post["/tweets/user-time-line/{value}"] = parameters =>
            {
                var id = long.Parse(parameters.value);
                var claims = (string) id.ToString();
                this.RequiresClaims(new[] { claims });

                var pageString = Request.Headers["Page"].First();
                if (!Request.Headers.Keys.Contains("LineHead"))
                {
                    return null;
                }
                var lineHeadString = Request.Headers["LineHead"].First();
                var page = int.Parse(pageString);
                var lineHead = long.MaxValue;
                try
                {
                    lineHead = long.Parse(lineHeadString);
                }
                catch (Exception)
                {
                    // ignored
                }

                
                var r = _storage.GetUserLine(id, page, 20, lineHead);
                JsonSettings.MaxJsonLength=int.MaxValue;
                var jsonBytes = Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(r));
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            };

            Post["/tweets/user-time-line/unshown/{value}"] = parameters =>
            {
                var id = long.Parse(parameters.value);
                var claims = (string)id.ToString();
                this.RequiresClaims(new[] { claims });

                var pageString = Request.Headers["Page"].First();
                if (!Request.Headers.Keys.Contains("LineHead"))
                {
                    return null;
                }
                var lineHeadString = Request.Headers["LineHead"].First();
                try
                {
                    var lastShown = Request.Headers["LastShown"].First();
                    _storage.SetLastShownId(id, ulong.Parse(lastShown));
                }
                catch (Exception)
                {
                }
                var page = int.Parse(pageString);
                var lineHead = long.MaxValue;
                try
                {
                    lineHead = long.Parse(lineHeadString);
                }
                catch (Exception)
                {
                    // ignored
                }


                var r = _storage.GetPageBefore(id, page, 10, lineHead);
                JsonSettings.MaxJsonLength = int.MaxValue;
                var jsonBytes = Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(r));
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            };

            Post["/tweets/user-time-line/last-shown/{value}"] = parameters =>
            {
                try
                {
                    var id = long.Parse(parameters.value);
                    var claims = (string) id.ToString();
                    this.RequiresClaims(new[] {claims});

                    var lastShown = Request.Headers["LastShown"].First();

                    _storage.SetLastShownId(id, ulong.Parse(lastShown));
                }
                catch (Exception e)
                {
                }
                return new Response();
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
