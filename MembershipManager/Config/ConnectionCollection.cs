using System.Configuration;

namespace MembershipManager.Config
{
    [ConfigurationCollection(typeof(ConnectionCollection), AddItemName = "add")]
    public class ConnectionCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConnectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConnectionElement)element).Name;
        }
    }
}
