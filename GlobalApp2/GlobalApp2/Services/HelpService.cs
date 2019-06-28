using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GlobalApp2.Services
{
    public class HelpService : IHelpService
    {
        //IStringLocalizer _aboutlocalizer;
        //IStringLocalizer _departmentlocalizer;

        IStringLocalizerFactory _factory;
        public HelpService(IStringLocalizerFactory factory)//IStringLocalizer<AboutService> aboutlocalizer,IStringLocalizer<DepartmentService> departmentlocalizer
        {
            _factory = factory;
            //_aboutlocalizer = aboutlocalizer;
            //_departmentlocalizer = departmentlocalizer;
        }
        public string GetHelpFor(string serviceName)
        {

            //Sürekli her service için yeni bir instance oluşturmak ve ona göre işlem yapmak yerine service typeını alıyoruz.

            string serviceClassName = $"{serviceName}Service";

            Type serviceType = Assembly.GetEntryAssembly()
                .ExportedTypes
                .Where(x => x.Name.Equals(serviceClassName, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault();

            if(serviceType == null)
            {
                return $"Help is not available for {serviceName}.";
            }

            IStringLocalizer localizer = _factory.Create(serviceType); // aldığımız service typeına göre burada o serviceden string localizer instanceı oluşturuyoruz.


            IEnumerable<LocalizedString> resources = localizer.GetAllStrings();

            //switch (serviceName)
            //{
            //    case "about":
            //        {
            //            resources = _aboutlocalizer.GetAllStrings();
            //            break;
            //            //IEnumerable<string> keys = resources.Select(x => x.Name);
            //            //return $"Avaible keys : {string.Join(",",keys)}";
            //        }
            //    case "department":
            //        {
            //            resources = _departmentlocalizer.GetAllStrings();
            //            break;
            //            //IEnumerable<string> keys = resources.Select(x => x.Name);
            //            //return $"Avaible keys : {string.Join(",",keys)}";
            //        }
            //    default:
            //        {
            //            return $"Help was not found for {serviceName}.";
            //        }
            //}

            IEnumerable<string> keys = resources.Select(x => x.Name);
            return $"Avaible keys : {string.Join(",",keys)}";
        }
    }
}
