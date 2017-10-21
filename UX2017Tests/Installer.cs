using System.Net.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UX2017;

namespace UX2017Tests
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IBarchartClient>().ImplementedBy<BarchartClient>(),
                Component.For<IJsonParser>().ImplementedBy<JsonParser>(),
                Component.For<INewsGenerator>().ImplementedBy<NewsGenerator>(),
                Component.For<IWordGenerator>().ImplementedBy<WordGenerator>(),
                Component.For<HttpClient>());
        }
    }
}
