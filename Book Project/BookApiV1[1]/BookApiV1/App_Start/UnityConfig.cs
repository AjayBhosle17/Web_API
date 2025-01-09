using BookApi.Models.BAL;
using BookApi.Models.Repositories;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace BookApiV1
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            container.RegisterType<IBookBL, BookBL>();
            container.RegisterType<IBookRepositories, BookRepositories>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
       
        }
    }
}