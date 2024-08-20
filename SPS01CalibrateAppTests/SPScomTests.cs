using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPS01CalibrateApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPS01CalibrateApp.Tests
{
    [TestClass()]
    public class SPScomTests
    {
        [TestMethod()]
        public void openTest()
        {
            var app = new SPScom();
            app.PortName = "COM8";
            app.open();
            app.ConnTs();
            //Assert.Fail();
        }
    }
}