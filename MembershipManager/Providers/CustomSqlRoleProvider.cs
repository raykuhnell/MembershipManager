using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;
using System.Web.Security;
using MembershipManager.Common;

namespace MembershipManager.Providers
{
    public class CustomSqlRoleProvider : SqlRoleProvider
    {
        public CustomSqlRoleProvider()
        {
            var config = new NameValueCollection();
            config.Add("connectionStringName", "PlaceholderConnectionString");
            this.Initialize(String.Empty, config);
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);

            var mc = MembershipConnection.Get();
            this.ApplicationName = mc.ApplicationName;

            var connectionString = String.Format("data source={0};initial catalog={1};user id={2};password={3};", mc.Server, mc.Database, mc.Username, mc.Password);
            FieldInfo connectionStringField = GetType().BaseType.GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            connectionStringField.SetValue(this, connectionString);
        }
    }
}