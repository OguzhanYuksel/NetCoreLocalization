using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalApp2.Services
{
    public interface IHomeService
    {
        IStringLocalizer DetectService(string serviceName);
    }
}
