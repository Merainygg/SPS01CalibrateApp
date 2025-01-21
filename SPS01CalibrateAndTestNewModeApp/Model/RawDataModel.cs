using System.Collections.Generic;

namespace SPS01CalibrateAndTestNewModeApp.Mode
{
    public class FullBrigeDataMode
    {
        
    }
    
    public class SpsCalibration
    {
        public string ID { get; set; }
        
        public string CalibraMode { get; set; }
        
        public bool Status = true;
        
        public Dictionary<string, double> FullBridgeTarget = new Dictionary<string, double>{{"O1",0},{"O2",0},{"O3",0},{"O4",0}};
        public Dictionary<string, double> HalfBridgeTarget = new Dictionary<string, double>{{"O1",0},{"O2",0},{"O3",0},{"O4",0}};

        public Dictionary<string, double> FullBridgeRawData = new Dictionary<string, double>{{"T0P1",0},{"T0P2",0},{"T0P3",0},{"T0P4",0},{"T1P1",0},{"T1P2",0},{"T2P1",0},{"T2P2",0},{"T3P1",0},{"T3P2",0}};
        public Dictionary<string, double> HalfBridgeRawData = new Dictionary<string, double>{{"T0P1",0},{"T0P2",0},{"T0P3",0},{"T0P4",0},{"T1P1",0},{"T1P2",0},{"T2P1",0},{"T2P2",0},{"T3P1",0},{"T3P2",0}};
        
        public Dictionary<string, double> TsiTempTarget = new Dictionary<string, double>{{"T0",0},{"T1",0},{"T2",0},{"T3",0}};
        public Dictionary<string, double> TsiTempRaw = new Dictionary<string, double>{{"T0",0},{"T1",0},{"T2",0},{"T3",0}};
        
        public Dictionary<string, double> TseTempTarget = new Dictionary<string, double>{{"T0",0},{"T1",0},{"T2",0},{"T3",0}};
        public Dictionary<string, double> TseTempRaw = new Dictionary<string, double>{{"T0",0},{"T1",0},{"T2",0},{"T3",0}};
        
        public Dictionary<string, double> FullBridgeFactor = new Dictionary<string, double>{{"s0",0},{"tsc1",0},{"tsc2",0},{"tsc3",0},{"tco1",0},{"tco2",0},{"tco3",0},{"f0",0},{"k2",0},{"k3",0},{"baseT",0}};
        public Dictionary<string, double> HalfBridgeFactor = new Dictionary<string, double>{{"s0",0},{"tsc1",0},{"tsc2",0},{"tsc3",0},{"tco1",0},{"tco2",0},{"tco3",0},{"f0",0},{"k2",0},{"k3",0},{"baseT",0}};

        public Dictionary<string, double> TsiFactor = new Dictionary<string, double>
            { { "k", 0 }, { "m", 0 }, { "Toff", 0 } };
        public Dictionary<string, double> TseFactor = new Dictionary<string, double>
            { { "k", 0 }, { "m", 0 }, { "Toff", 0 } };
        
        public string[] NvmData { get; set; }
    }
}