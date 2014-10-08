using System.Windows;

namespace MembershipManager.Common
{
    public class MembershipConnection
    {
        public string Server;
        public string Database;
        public string Username;
        public string Password;
        public string ApplicationName;

        public static MembershipConnection Get ()
        {
            return (MembershipConnection)Application.Current.Properties["Connection"];
        }

        public static void Set(string server, string database, string username, string password, string applicationName)
        {
            Set(new MembershipConnection
            {
                Server = server,
                Database = database,
                Username = username,
                Password = password,
                ApplicationName = applicationName
            });
        }
        public static void Set(MembershipConnection mc)
        {
            Application.Current.Properties.Add("Connection", mc);
        }
    }
}
