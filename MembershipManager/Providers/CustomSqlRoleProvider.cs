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
        public override void Initialize(string name, NameValueCollection config)
        {
            var mc = MembershipConnection.GetCurrent();
            config.Set("applicationName", mc.ApplicationName);
            config.Set("connectionStringName", "DummyConnectionString");

            base.Initialize(name, config);

            var connectionString = String.Format("data source={0};initial catalog={1};user id={2};password={3};", mc.Server, mc.Database, mc.Username, mc.Password);
            FieldInfo connectionStringField = GetType().BaseType.GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            connectionStringField.SetValue(this, connectionString);
        }
    }
}