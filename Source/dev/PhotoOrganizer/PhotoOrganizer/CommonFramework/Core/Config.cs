using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CommonFramework.Core
{
    public partial class Config
    {


        const char CONFIG_STRING_SEPARATOR = '|';


        public string GetConfigValue(string key)
        {
            string result = string.Empty;
            try
            {
                result = System.Configuration.ConfigurationManager.AppSettings[key].ToString();
            }
            catch (System.Exception ex)
            {
                throw new Exception.TechnicalException(string.Format(CommonFramework.Resources.ErrorFetchingConfigurationValueForKey0, key), ex);

            }

            return result;
        }

        public string GetConfigValue(string key, string defaultValue)
        {
            string result = GetConfigValue(key);
            if (result == string.Empty)
                result = defaultValue;

            return result;

        }



        public List<string> GetConfigValueAsList(string key)
        {
            return Helper.StringConvert.StringToList(GetConfigValue(key), CONFIG_STRING_SEPARATOR);
        }
    }
}
