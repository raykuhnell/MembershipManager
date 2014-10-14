using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;

namespace MembershipManager.Common
{
    public class MembershipConnection
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ApplicationName { get; set; }

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
            Application.Current.Properties.Remove("Connection");
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

        public static void Clear()
        {
            Application.Current.Properties.Remove("Connection");
        }

        public bool Test()
        {
            bool success = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(this.ConnectionString()))
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
            return String.Format("<connection name=\"{0}\" connectionString=\"{1}\" applicationName=\"{2}\" />", Name, this.ConnectionString(), ApplicationName);
        }

        public string ConnectionString()
        {
            var builder = new SqlConnectionStringBuilder();

            builder.DataSource = Server;
            builder.InitialCatalog = Database;
            builder.UserID = Username;
            builder.Password = Password;

            return builder.ConnectionString;
        }
    }
}
