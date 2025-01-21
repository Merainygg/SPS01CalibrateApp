using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPS01CalibrateAndTestNewModeApp.Core;
using SPS01CalibrateAndTestNewModeApp.Mode;

namespace SPS01CalibrateAndTestNewModeApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ServiceContainer.Register(new EvbSerialModel());
            ServiceContainer.Register(new SpsCalibration());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}