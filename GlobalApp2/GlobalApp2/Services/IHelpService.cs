using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalApp2.Services
{
    interface IHelpService
    {
        string GetHelpFor(string serviceName);
    }
}
