using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SPS01CalibrateAndTestNewModeApp.Core;
using SPS01CalibrateAndTestNewModeApp.Mode;

namespace SPS01CalibrateAndTestNewModeAppTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var pressure = new double[] { 775640, 2636214, 4504033 };
            var temperature = new double[] { 20 };
            var target = new double[] { 11, 0.5, 0.9 };
            CalcutionPxxx calc = new CalcutionPxxx(pressure.ToList(),temperature.ToList(),target.ToList(),"1T3P", "long", "p100");
            calc.SolveAbc();
            foreach(var item in calc.abcd)
            {
                Console.WriteLine(item);
            }
            calc.AbcdFour();
            foreach(var item in calc.abcd)
            {
                Console.WriteLine(item);
            }
            calc.SolveF0();
            Console.WriteLine(calc.f0);
        }

        [Test]
        public async Task Test2()
        {
            var sps = new SpsCalibration();
            // { "T0P1", 0 }, { "T0P2", 0 }, { "T0P3", 0 }, { "T0P4", 0 }, { "T1P1", 0 }, { "T1P2", 0 }, { "T2P1", 0 },
            // { "T2P2", 0 }, { "T3P1", 0 }, { "T3P2", 0 }
            sps.HalfBridgeRawData["T0P1"] = 447;
            sps.HalfBridgeRawData["T0P2"] = 14721;
            sps.HalfBridgeRawData["T0P3"] = 4026;
            //  { { "O1", 0 }, { "O2", 0 }, { "O3", 0 }, { "O4", 0 } };
            sps.HalfBridgeTarget["O1"] = 0.1;
            sps.HalfBridgeTarget["O2"] = 0.9;
            sps.HalfBridgeTarget["O3"] = 0.3;
            // sps.HalfBridgeTarget["O4"] = 0.4
            sps.TsiTempRaw["T1"] = 25;
            sps.CalibraMode = "1T3P";
            
            var result = sps.HalfBridgeDataToJson();
            Console.WriteLine(result);
            var jsonStr = await sps.Calibration(result);
            
            
            sps.JsonToHalfBridgeFactor(jsonStr);
            Console.WriteLine(sps.ToJson());
        }
        
    }
}