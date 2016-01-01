using Nancy.Authentication.Token;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Repository;
using Server;

namespace SelfHostedRestAPI
{
    using Nancy;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {

            container.Register<IStorage, Storage>().AsSingleton();
            container.Register<CredentialsStorage>().AsSingleton();
            var serverScheduler = new ServerScheduler(container.Resolve<IStorage>());
            serverScheduler.Start();

        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            TokenAuthentication.Enable(pipelines, new TokenAuthenticationConfiguration(container.Resolve<ITokenizer>()));
            //CORS Enable
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers",
                        "Accept, Origin, Content-type, access-control-allow-origin, password, username, authorization, email, linehead, page, x-pagination,UserId");

            });

        }
    }
}