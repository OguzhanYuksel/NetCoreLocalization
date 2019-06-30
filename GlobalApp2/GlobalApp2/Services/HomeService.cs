using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GlobalApp2.Services
{
    public class HomeService : IHomeService
    {
        IStringLocalizerFactory _factory;
        public HomeService(IStringLocalizerFactory factory)//IStringLocalizer<AboutService> aboutlocalizer,IStringLocalizer<DepartmentService> departmentlocalizer
        {
            _factory = factory;
            //_aboutlocalizer = aboutlocalizer;
            //_departmentlocalizer = departmentlocalizer;
        }

        public IStringLocalizer DetectService(string serviceName)
        {
            string serviceClassName = $"{serviceName}Service";

            Type serviceType = Assembly.GetEntryAssembly()
                .ExportedTypes
                .Where(x => x.Name.Equals(serviceClassName, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault();

            if (serviceType == null)
            {
                return null;
            }
            return _factory.Create(serviceType);
        }
    }
}
