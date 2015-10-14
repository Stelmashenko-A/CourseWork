using System.Collections.Generic;
using Nancy;
using Nancy.Json;
using TestData;

namespace SelfHostedRestAPI
{
    public class FilterModule : NancyModule
    {
        public FilterModule()
        {
            InitializeFilter();
        }

        protected void InitializeFilter()
        {
            Get["/filter"] = parameters =>
            {
                var l = new List<Test> { new Test("F1"), new Test("F2"), new Test("f3") };
                return new JavaScriptSerializer().Serialize(l);
            };
        }
    }
}
