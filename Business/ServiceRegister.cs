using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ServiceRegister : Autofac.Module
    {
        public IConfiguration Configuration { get; set; }
        private readonly List<Assembly> _assemblies = new List<Assembly>();

        public ServiceRegister(IConfiguration configuration, Assembly? callingAssembly = null)
        {
            var coreAssembly = Assembly.GetAssembly(typeof(ExcelManager));
            if (coreAssembly != null)
            {
                _assemblies.Add(coreAssembly);
            }

            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
        }
        protected override void Load(ContainerBuilder builder)
        {
            RegisterServicesAndConfig(builder);
        }
        private void RegisterServicesAndConfig(ContainerBuilder builder)
        {
           builder.RegisterType<ExcelManager>().As<ExcelManager>()
                .InstancePerLifetimeScope();

            builder.RegisterType<LoginManager>().As<LoginManager>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PersonelManager>().As<PersonelManager>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DokumanManager>().As<DokumanManager>()
                .InstancePerLifetimeScope();
        }
    }
}
