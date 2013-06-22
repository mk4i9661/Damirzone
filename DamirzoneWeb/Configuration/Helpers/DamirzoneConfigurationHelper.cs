using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamirzoneWeb.Configuration.Helpers
{
    public class DamirzoneConfigurationHelper
    {
        public DamirzoneConfigurationHelper(DamirzoneConfigurationSection section) {
            ImageFolderPath = section.ImageFolderPath;
        }

        public string ImageFolderPath {
            get;
            protected set;
        }
    }
}