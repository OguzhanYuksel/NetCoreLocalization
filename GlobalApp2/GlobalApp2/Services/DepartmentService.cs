using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalApp2.Services
{
    public class DepartmentService : IDepartmentService
    {
        IStringLocalizer<DepartmentService> _localizer;

        public DepartmentService(IStringLocalizer<DepartmentService> localizer)
        {
            _localizer = localizer;
        }
        public string GetInfo(string name)
        {
            LocalizedString value = _localizer[name];
            if(value.ResourceNotFound)
            {
                return _localizer["help"];
            }
            return value;
        }
    }
}
