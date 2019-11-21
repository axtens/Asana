using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AsanaTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Instantiate()
        {
            var asana = new Asana.Asana(false);
            Assert.AreEqual("Asana", asana.GetType().Name);
        }

        [TestMethod]
        public void SetVar()
        {
            var asana = new Asana.Asana();
            asana.SetStringVar("name", "value");
            Assert.AreEqual("value", asana.GetStringVar("name"));
        }

        [TestMethod]
        public void GetVar()
        {
            var asana = new Asana.Asana();
            Assert.IsNull(asana.GetStringVar("name"));
        }

        [TestMethod]
        public void Token()
        {
            var asana = new Asana.Asana();
            asana.SetToken("YOUR_TOKEN");
            var tasks = asana.Get("tasks");
            Assert.IsTrue(tasks.ToString().Contains("error"));
        }

        [TestMethod]
        public void GetTasks()
        {
            var asana = new Asana.Asana();
            asana.SetToken("YOUR_TOKEN");
            asana.SetStringVar("{project_gid}", "BIG_NUMBER");
            var tasks = asana.Get("projects/{project_gid}/tasks",true);
            Assert.IsTrue(tasks.Length > 0);
        }

        [TestMethod]
        public void GetProjects()
        {
            var asana = new Asana.Asana(false);
            asana.SetToken("YOUR_TOKEN");
            asana.SetStringVar("{project_gid}", "BIG_NUMBER");
            asana.SetBooleanVar("pretty", true);
            var tasks = asana.Get("projects");
            Assert.IsTrue(tasks.Length > 0);
        }
    }
}
