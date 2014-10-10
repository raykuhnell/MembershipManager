using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MembershipManager.Common;

namespace MembershipManager.Tests
{
    [TestClass]
    public class MembershipConnectionTests
    {
        [TestMethod]
        public void GetFromString_ValidString()
        {
            string connectionString = "<add name=\"Name\" connectionString=\"Data Source=Server;Initial Catalog=Database;User ID=Username;Password=Password;\" applicationName=\"ApplicationName\" />";
            var mc = MembershipConnection.GetFromString(connectionString);
            Assert.AreEqual("Name", mc.Name);
            Assert.AreEqual("Server", mc.Server);
            Assert.AreEqual("Database", mc.Database);
            Assert.AreEqual("Username", mc.Username);
            Assert.AreEqual("Password", mc.Password);
            Assert.AreEqual("ApplicationName", mc.ApplicationName);
        }
    }
}
