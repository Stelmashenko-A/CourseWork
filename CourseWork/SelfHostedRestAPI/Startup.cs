﻿using Owin;
using System.Collections.Generic;
using Owin.StatelessAuth;

namespace SelfHostedRestAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
            app.RequiresStatelessAuth(
                new MySecureTokenValidator(new ConfigProvider()),
                new StatelessAuthOptions() { IgnorePaths = new List<string>(new[] { "/login", "/content" }) })
                .UseNancy();

        }
    }
}