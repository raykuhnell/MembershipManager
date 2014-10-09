using System.Configuration;

namespace MembershipManager.Config
{
    public class ConnectionsSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ConnectionCollection Connections
        { 
            get 
            {
                return (ConnectionCollection)base[""]; 
            } 
        }
    }
}
