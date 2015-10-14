namespace SelfHostedRestAPI
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
            Post["/"] = parameters =>
            {
                return View["index"];
            };
        }
    }
}