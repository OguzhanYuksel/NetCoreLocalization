using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalApp2.Localization
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        string _resourcesRelativePath;
        string _typeRelativeNamespace;
        CultureInfo _uiCulture;
        JObject _resourceCache;
        public JsonStringLocalizer(string resourcesRelativePath,string typeRelativeNamespace,CultureInfo uiCulture)
        {
            _resourcesRelativePath = resourcesRelativePath;
            _typeRelativeNamespace = typeRelativeNamespace;
            _uiCulture = uiCulture;
        }

        JObject GetResource()
        {
            if(_resourceCache == null)
            {
                string tag = _uiCulture.Name; // provided culture tag name

                string typeRelativePath = _typeRelativeNamespace.Replace(".", "/");

                string filePath = $"{_resourcesRelativePath}{typeRelativePath}/{tag}.json";

                string json = File.Exists(filePath) ? File.ReadAllText(filePath, Encoding.UTF8) : "{}";

                 _resourceCache = JObject.Parse(json);
            }
            return _resourceCache; //eger _resourceCache boşsa jsonı baştan oku. Aynı işlem için jsonı baştan okuyup parse etme.
        }

        public LocalizedString this[string name]
        {
            get
            {
                return this[name, null];
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                JObject resources = GetResource();
                string value = resources.Value<string>(name);
                bool resourcesNotFound = string.IsNullOrWhiteSpace(value);

                if(resourcesNotFound)
                {
                    value = name;
                }
                else
                {
                    if (arguments != null)
                    {
                        value = string.Format(value, arguments);
                    }
                }
                return new LocalizedString(name, value,resourcesNotFound);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            JObject resources = GetResource();
            foreach (var pair in resources)
            {
                yield return new LocalizedString(pair.Key,pair.Value.Value<string>());
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new JsonStringLocalizer(_resourcesRelativePath,_typeRelativeNamespace,culture);
        }
    }
}
