using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace DamirzoneWeb.Configuration
{
    public class DamirzoneConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("imageFolderPath", DefaultValue = "C:/inetpub/wwwroot/damirzone/images", IsRequired = true)]
        public string ImageFolderPath {
            get {
                return (string)this["imageFolderPath"];
            }
            set {
                this["imageFolderPath"] = value;
            }
        }
    }
}