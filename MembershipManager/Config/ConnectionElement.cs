using System;
using System.Configuration;

namespace MembershipManager.Config
{
    public class ConnectionElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public String Name
        {
            get
            {
                return (String)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("applicationName", IsRequired = true)]
        public String ApplicationName
        {
            get
            {
                return (String)this["applicationName"];
            }
            set
            {
                this["applicationName"] = value;
            }
        }

        [ConfigurationProperty("connectionStringName", IsRequired = true)]
        public String ConnectionStringName
        {
            get
            {
                return (String)this["connectionStringName"];
            }
            set
            {
                this["connectionStringName"] = value;
            }
        }
    }
}
