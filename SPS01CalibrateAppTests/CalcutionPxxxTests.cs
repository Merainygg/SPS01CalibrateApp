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
    public class CalcutionPxxxTests
    {
        [TestMethod()]
        public void SolveAbcTest()
        {
            var pressure = new double[] { 775640, 2636214, 4504033 };
            var temperature = new double[] { 20 };
            var target = new double[] { 0.1, 0.5, 0.9 };
            CalcutionPxxx calc = new CalcutionPxxx(pressure.ToList(),temperature.ToList(),target.ToList(),"1T3P", "long", "P100");
            calc.SolveAbc();
            foreach(var item in calc.abcd)
            {
                Console.WriteLine(item);
            }
            //Assert.Fail();
        }
    }
}