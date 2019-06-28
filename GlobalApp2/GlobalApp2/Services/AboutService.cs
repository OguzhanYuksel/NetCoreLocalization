using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalApp2.Services
{
    public class AboutService : IAboutService
    {
        IStringLocalizer<AboutService> _localizer;
        public AboutService(IStringLocalizer<AboutService> localizer)
        {
            _localizer = localizer;
            Console.WriteLine(_localizer.GetType());
        }
        public string Reply(string searchTerm)
        {
            LocalizedString resource = _localizer[searchTerm];
            if(resource.ResourceNotFound)
            {
                return _localizer["help"]; // bulamazsa help service'e gidecek.
            }
            return resource;
        }
    }
}
