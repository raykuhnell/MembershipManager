using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;

namespace MembershipManager.Common
{
    public class MembershipConnection
    {
        public string Name;
        public string Server;
        public string Database;
        public string Username;
        public string Password;
        public string ApplicationName;

        public static MembershipConnection GetCurrent ()
        {
            return (MembershipConnection)Application.Current.Properties["Connection"];
        }

        public static void SetCurrent(string server, string database, string username, string password, string applicationName)
        {
            SetCurrent(new MembershipConnection
            {
                Server = server,
                Database = database,
                Username = username,
                Password = password,
                ApplicationName = applicationName
            });
        }
        public static void SetCurrent(MembershipConnection mc)
        {
            Application.Current.Properties.Add("Connection", mc);
        }

        public static MembershipConnection GetFromString(string value)
        {
            var matches = Regex.Matches(value, "\\w+=\"([^\"]*)\"");

            var builder = new SqlConnectionStringBuilder(matches[1].Groups[1].Value);

            return new MembershipConnection
            {
                Name = matches[0].Groups[1].Value,
                Server = builder.DataSource,
                Database = builder.InitialCatalog,
                Username = builder.UserID,
                Password = builder.Password,
                ApplicationName = matches[2].Groups[1].Value
            };
        }

        public bool Test()
        {
            bool success = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(this.ToString()))
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }

        public override string ToString()
        {
            var builder = new SqlConnectionStringBuilder();
            
            builder.DataSource = Server;
            builder.InitialCatalog = Database;
            builder.UserID = Username;
            builder.Password = Password;

            return String.Format("<connection name=\"{0}\" connectionString=\"{1}\" applicationName=\"{2}\" />", Name, builder.ConnectionString, ApplicationName);
        }
    }
}
