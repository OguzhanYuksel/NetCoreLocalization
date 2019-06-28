using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GlobalApp2.Localization
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        string _resourcesRelativePath;
        public JsonStringLocalizerFactory(IOptions<JsonLocalizationOptions> options)
        {
            _resourcesRelativePath = options.Value?.ResourcesPath ?? string.Empty;
        }
        public IStringLocalizer Create(Type resourceSource)
        {
            TypeInfo typeInfo = resourceSource.GetTypeInfo();

            AssemblyName assemblyName = new AssemblyName(typeInfo.Assembly.FullName);

            string baseNamespace = assemblyName.Name;

            string typeRelativeNamespace = typeInfo.FullName.Substring(baseNamespace.Length);

            return new JsonStringLocalizer(_resourcesRelativePath,typeRelativeNamespace,CultureInfo.CurrentUICulture);

        }

        public IStringLocalizer Create(string baseName, string location)
        {
            throw new NotImplementedException();
        }
    }
}
